using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameShortcuts : MonoBehaviour
{
    public Camera mainVR;
    public Camera ceiling;
    public Camera wall;
    public Camera follow;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            mainVR.targetDisplay = 0;
            ceiling.targetDisplay = 1;
            wall.targetDisplay = 2;
            follow.targetDisplay = 3;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            mainVR.targetDisplay = 1;
            ceiling.targetDisplay = 0;
            wall.targetDisplay = 2;
            follow.targetDisplay = 3;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            mainVR.targetDisplay = 2;
            ceiling.targetDisplay = 1;
            wall.targetDisplay = 0;
            follow.targetDisplay = 3;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            mainVR.targetDisplay = 3;
            ceiling.targetDisplay = 1;
            wall.targetDisplay = 2;
            follow.targetDisplay = 0;
        }
    }
}
