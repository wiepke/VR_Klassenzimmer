using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class laserHandler : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private SteamVR_TrackedController trackedController;

    private GameObject lastFocusedObject = null;

    private void OnEnable()
    {
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut -= HandlePointerOut;
        laserPointer.PointerOut += HandlePointerOut;

        trackedController = GetComponent<SteamVR_TrackedController>();
        if (trackedController == null)
        {
            trackedController = GetComponentInParent<SteamVR_TrackedController>();
        }
        trackedController.TriggerClicked -= HandleTriggerClicked;
        trackedController.TriggerClicked += HandleTriggerClicked;
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (lastFocusedObject != null)
        {
            GameObject student = lastFocusedObject;
            if (ConfigureStudent.studentAttributes[student].isDistorting)
            {
                MenuDataHolder.MisbehaviourSeen++;
                ConfigureStudent.triggerLastGoodBehaviour(student);
            }
        }
            if (EventSystem.current.currentSelectedGameObject != null)
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "Student")
        {
            lastFocusedObject = e.target.gameObject;
            //recognition of misbhehaviour here
            Debug.Log("HandlePointerIn" + e.target.name);
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "Student")
        {
            lastFocusedObject = null;
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("HandlePointerOut" + e.target.name);
        }
    }
}