using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetupClass : MonoBehaviour
{
    public Transform Teacher;
    void Start()
    {
        AllStudentAttributes.Teacher = Teacher;
        AllStudentAttributes.SetupStudentValues();
        ConfigLoader.SetupPosters();
        ConfigLoader.SetupLessonDraft();
        ClassController.Handler = GetComponent<SocketEventHandler>();
    }
}