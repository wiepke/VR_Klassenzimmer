using UnityEngine;
using UnityEngine.XR;

public class ControllerTooltip : MonoBehaviour
{
    public GameObject ControllerModel;
    public MasterController mc;

    private GameObject ThumbTooltip;
    private GameObject GrabTooltip;
    private GameObject MenuTooltip;

    private bool GrabToolTipActive = true;
    private bool TeleportToolTipActive = true;
    private bool MenuToolTipActive = true;

    // Start is called before the first frame update
    void Start()
    {
        ThumbTooltip = ControllerModel.transform.Find("ToolTipThumb").gameObject;
        GrabTooltip = ControllerModel.transform.Find("ToolTipGrab").gameObject;
        MenuTooltip = ControllerModel.transform.Find("ToolTipMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GrabToolTipActive)
        {
            float grabButtonPressed = 0;
            mc.m_LeftInputDevice.TryGetFeatureValue(CommonUsages.grip, out grabButtonPressed);
            if (grabButtonPressed > 0.5f)
            {
                GrabTooltip.SetActive(false);
                GrabToolTipActive = false;
            }
        }

        if (TeleportToolTipActive)
        {
            bool ThumbDown = false;
            mc.m_LeftInputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out ThumbDown);
            if (ThumbDown)
            {
                ThumbTooltip.SetActive(false);
                TeleportToolTipActive = false;
            }
        }

        if (MenuToolTipActive)
        {
            bool menuButtonPressed = false;
            mc.m_LeftInputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out menuButtonPressed);
            if (menuButtonPressed)
            {
                MenuTooltip.SetActive(false);
                MenuToolTipActive = false;
            }
        }

        if(!GrabTooltip && !TeleportToolTipActive && !MenuToolTipActive)
        {
            ControllerModel.SetActive(false);
        }

    }
}
