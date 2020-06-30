using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    public Transform LeftStudent, RightStudent;
    public string Id;

    private void Start()
    {
        LeftStudent.GetComponent<BehaviourController>().SetConversationPartner(RightStudent);
        RightStudent.GetComponent<BehaviourController>().SetConversationPartner(LeftStudent);
    }

    public void SetStudentIds()
    {
        LeftStudent.GetComponent<StudentController>().Id = Id + "L";
        RightStudent.GetComponent<StudentController>().Id = Id + "R";
    }
}
