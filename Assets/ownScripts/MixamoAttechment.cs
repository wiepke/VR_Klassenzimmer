using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixamoAttechment : MonoBehaviour
{
    [SerializeField]
    GameObject whatToHold = null;

    private Transform attachmentPointRight;
    private Transform attachmentPointLeft;
    private bool isRight = true;

    // Start is called before the first frame update
    void Start()
    {
        attachmentPointRight = gameObject.transform.
            Find("mixamorig:Hips").Find("mixamorig:Spine").
            GetChild(0).GetChild(0).
            Find("mixamorig:RightShoulder").
            GetChild(0).GetChild(0).GetChild(0);
        attachmentPointLeft = gameObject.transform.
            Find("mixamorig:Hips").Find("mixamorig:Spine").
            GetChild(0).GetChild(0).
            Find("mixamorig:LeftShoulder").
            GetChild(0).GetChild(0).GetChild(0);
    }

    public void attachRight(GameObject attachment)
    {
        isRight = true;
        whatToHold = Instantiate(attachment);
    }
    public void attachLeft(GameObject attachment)
    {
        isRight = false;
        whatToHold = Instantiate(attachment);
    }

    public void detach()
    {
        if (whatToHold != null)
            Destroy(whatToHold);
        whatToHold = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (whatToHold != null)
        {
            switch (whatToHold.name)
            {
                case "Cola Can(Clone)":
                    if (isRight)
                    {
                        whatToHold.transform.position = attachmentPointRight.position
                        + attachmentPointRight.up * 0.21f
                        + attachmentPointRight.forward * 0.07f
                        + attachmentPointRight.right * 0.1f;
                        whatToHold.transform.localEulerAngles = attachmentPointRight.eulerAngles + new Vector3(0, 0, 90);
                    }
                    else
                    {
                        whatToHold.transform.position = attachmentPointLeft.position
                        + attachmentPointLeft.up * 0.21f
                        + attachmentPointLeft.forward * 0.07f
                        + attachmentPointLeft.right * (-0.1f);
                        whatToHold.transform.localEulerAngles = attachmentPointLeft.eulerAngles - new Vector3(0, 0, 90);
                    }
                    break;
                case "pen(Clone)":
                    if (isRight)
                    {
                        whatToHold.transform.position = attachmentPointRight.position
                        + attachmentPointRight.up * 0.21f
                        + attachmentPointRight.forward * 0.12f
                        + attachmentPointRight.right * -0.07f;
                        whatToHold.transform.localEulerAngles = attachmentPointRight.eulerAngles + new Vector3(200, 10, 0);
                    }
                    else
                    {
                        whatToHold.transform.position = attachmentPointLeft.position
                        + attachmentPointLeft.up * 0.21f
                        + attachmentPointLeft.forward * 0.07f
                        + attachmentPointLeft.right * (-0.1f);
                        whatToHold.transform.localEulerAngles = attachmentPointLeft.eulerAngles - new Vector3(180, 0, 0);
                    }
                    break;
                default:
                    break;
            }
            
            
        }
    }
}