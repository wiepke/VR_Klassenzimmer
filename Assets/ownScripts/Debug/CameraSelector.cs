using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class CameraSelector : MonoBehaviour
{
    bool needsReset = false;
    List<InputDevice> foundHMDs = new List<InputDevice>();
    [SerializeField] private GameObject fallbackCamGameObject = default;
    [SerializeField] private GameObject VRCamGameObject = default;
    [SerializeField] private GameObject VRLeftHandGameObject = default;
    [SerializeField] private GameObject VRRightHandGameObject = default;
    [SerializeField] private Transform SpawnPoint = default;

    [HideInInspector] public GameObject activeCameraObject { get; set; }
    public bool fallbackCamIsActive { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, foundHMDs);
        activeCameraObject = useFallbackCam();
    }

    // Update is called once per frame
    void Update()
    {
        handleActiveCam();
    }

    private void handleActiveCam(){
        if (foundHMDs.Count == 0){
            activeCameraObject = useFallbackCam();
        }else{
            foundHMDs[0].TryGetFeatureValue(CommonUsages.userPresence, out bool userPresent);
            if (!userPresent)
            {
                activeCameraObject = useFallbackCam();
            }
            else
            {
                activeCameraObject = useVRCam();
            }
        }
        AllStudentAttributes.Teacher = activeCameraObject.transform;
    }

    private GameObject useFallbackCam(){
        fallbackCamIsActive = true;
        activateCamAndHands(fallbackCamIsActive);
        needsReset = true;
        return fallbackCamGameObject;
    }

    private GameObject useVRCam(){
        fallbackCamIsActive = false;
        activateCamAndHands(fallbackCamIsActive);
        // if user uses HMD reset Rotation
        if (needsReset)
        {
            fallbackCamGameObject.GetComponent<MockHMDInteraction>().rotateView(new Vector3(0, 0, 0));
            VRCamGameObject.transform.position = SpawnPoint.position;
            needsReset = false;
        }
        return VRCamGameObject;
    }

    private void activateCamAndHands(bool fallbackIsActive)
    {
        fallbackCamGameObject.SetActive(fallbackIsActive);
        VRCamGameObject.SetActive(!fallbackIsActive);
        VRLeftHandGameObject.SetActive(!fallbackIsActive);
        VRRightHandGameObject.SetActive(!fallbackIsActive);
    }
}
