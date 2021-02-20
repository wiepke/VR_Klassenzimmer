using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    public Transform LeftStudent, RightStudent;
    public string Id;

    private void Start()
    {
        // Register students with their desk neighbours
        LeftStudent.GetComponent<BehaviourController>().ConversationPartner = RightStudent;
        RightStudent.GetComponent<BehaviourController>().ConversationPartner = LeftStudent;
    }

    public void SetStudentIds()
    {
        // Assign IDs (based on desk ID, like in previous version)
        LeftStudent.GetComponent<StudentController>().Id = Id + "L";
        RightStudent.GetComponent<StudentController>().Id = Id + "R";
    }
}
