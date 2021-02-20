using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera cam;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObject = null;
    private PointerEventData pointerData = null;

    protected override void Awake()
    {
        base.Awake();
        pointerData = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        pointerData.Reset();
        pointerData.position = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);

        eventSystem.RaycastAll(pointerData, m_RaycastResultCache);
        pointerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = pointerData.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(pointerData, currentObject);

        if (clickAction.GetStateDown(targetSource))
        {
            ProcessPress(pointerData);
        }

        if (clickAction.GetStateUp(targetSource))
        {
            ProcessRelease(pointerData);
        }
    }

    public PointerEventData GetData()
    {
        return pointerData;
    }

    private void ProcessPress(PointerEventData data)
    {
        pointerData.pointerPressRaycast = pointerData.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, pointerData, ExecuteEvents.pointerDownHandler);
        
        if(newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }
        pointerData.pressPosition = pointerData.position;
        pointerData.pointerPress = newPointerPress;
        pointerData.rawPointerPress = currentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        ExecuteEvents.Execute(pointerData.pointerPress, pointerData, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        if(pointerData.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(pointerData.pointerPress, pointerData, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);

        pointerData.pressPosition = Vector2.zero;
        pointerData.pointerPress = null;
        pointerData.rawPointerPress = null;
    }
}
