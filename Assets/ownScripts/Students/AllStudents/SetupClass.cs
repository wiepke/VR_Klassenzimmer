using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetupClass : MonoBehaviour
{
    void Start()
    {
        AllStudentAttributes.Teacher = GameObject.Find("HeadCollider").transform;
        AllStudentAttributes.SetupStudentValues();
        ConfigLoader.SetupPosters();
        ConfigLoader.SetupLessonDraft();
        ClassController.Handler = GetComponent<SocketEventHandler>();
    }
}
