using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.Extras;


public class InGameShortcuts : MonoBehaviour
{
    public Camera mainVR;
    public Camera ceiling;
    public Camera wall;
    public Camera follow;

    public SteamVR_Action_Boolean menuButton;
    public SteamVR_Action_Boolean LaserPointerActivate;

    public GameObject LaserPointerGameObject;
    private bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        KeyboardControls();
        if (menuButton != null)
        {
            ControllerControls();
        }
    }

    private void KeyboardControls()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("VR-self").GetComponent<vrselfControl>().EndPositionTracking();
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            mainVR.targetDisplay = 0;
            ceiling.targetDisplay = 1;
            ceiling.rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
            wall.targetDisplay = 1;
            wall.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
            follow.targetDisplay = 1;
            follow.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            mainVR.targetDisplay = 0;
            ceiling.targetDisplay = 1;
            ceiling.rect = new Rect(0.0f, 0.1f, 1.0f, 0.9f);
            wall.targetDisplay = 1;
            wall.rect = new Rect(0.0f, 0.0f, 0.5f, 0.1f);
            follow.targetDisplay = 1;
            follow.rect = new Rect(0.5f, 0.0f, 0.5f, 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            mainVR.targetDisplay = 0;
            ceiling.targetDisplay = 1;
            ceiling.rect = new Rect(0.0f, 0.9f, 1.0f, 0.1f);
            wall.targetDisplay = 1;
            wall.rect = new Rect(0.0f, 0.0f, 0.9f, 0.9f);
            follow.targetDisplay = 1;
            follow.rect = new Rect(0.9f, 0.0f, 0.1f, 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            mainVR.targetDisplay = 0;
            ceiling.targetDisplay = 1;
            ceiling.rect = new Rect(0.0f, 0.9f, 1.0f, 0.1f);
            wall.targetDisplay = 1;
            wall.rect = new Rect(0.0f, 0.0f, 0.1f, 0.1f);
            follow.targetDisplay = 1;
            follow.rect = new Rect(0.1f, 0.0f, 0.9f, 0.9f);
        }
    }

    private void ControllerControls()
    {
        /*               default is not defined due to missing connection to a VR Headset
         *                                  "Teleport" resembles the Input-Name of your targeted Input
         *                                           "GetStateDown" looks into the boolean, when it goes from false to true
         *                                                         checks which controller is watched
        if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Source.Any))
        {
            print("Teleport triggert");
        }
        */
        if (menuButton.GetStateDown(SteamVR_Input_Sources.Any))
        {
            SceneManager.LoadScene(0);
        }
        
        /*if (LaserPointerActivate.GetStateDown(SteamVR_Input_Sources.Any))
        {
            isActive = !isActive;
            LaserPointerGameObject.SetActive(isActive);
        }*/
        
        /* checks any controller for SteamVR_Action_Vector2 properties to GetAxis and save in touchpadValue
        Vector2 touchpadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);
        if(touchpadValue!=Vector2.zero){
            print(touchpadValue);
        }
        */

    }
}
