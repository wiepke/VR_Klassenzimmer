using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionTrigger : MonoBehaviour
{
    private bool sitsLeft;
    private GameObject chattingStudent;
    private GameObject student;
    private Animator disturb;

    public void SetDisturbance(string studentPlace, string stoerung)
    {
        if (chattingStudent == null)
        {
            chattingStudent = GameObject.Find("Sun");
        }
        student = GameObject.Find("classroom-scaler/Students/" + studentPlace);
        
        sitsLeft = studentPlace.Contains("L");
        try
        {
            student = student.transform.GetChild(0).gameObject;
        }
        catch (System.Exception)
        {
            Debug.Log("student "+studentPlace+" not found");
            return;
        }
        
        ConfigureStudent.studentAttributes[student].ChanceToMisbehave = 0f;
        ConfigureStudent.studentAttributes[student].TimeDelayToLastMisbehaviour = DateTime.Now.Minute * 60 + DateTime.Now.Second;
        if (stoerung == "breathing" || stoerung == "writing")
        {
            ConfigureStudent.studentAttributes[student].LastGoodBehaviour = stoerung;
            ConfigureStudent.studentAttributes[student].isDistorting = false;
        }
        else
        {
            ConfigureStudent.studentAttributes[student].isDistorting = true;
            MenuDataHolder.MisbehaviourCount = MenuDataHolder.MisbehaviourCount + 1;
        }
        disturb = student.GetComponent<Animator>();
        if (stoerung.Equals("chatting"))
        {
            if (Math.Abs(chattingStudent.transform.position.x - student.transform.position.x) < 1.5)
            {
                if (chattingStudent.transform.position.z > student.transform.position.z + 2.0)
                {
                    stoerung += "Back";
                }
                if (chattingStudent.transform.position.z < student.transform.position.z + 2.0)
                {
                    stoerung += "For";

                }
            }
            if (sitsLeft)
            {
                stoerung += "L";
            }
            else
            {
                stoerung += "R";
            }
            chattingStudent = student;
        }

        if (stoerung.Equals("hit"))
        {
            if (sitsLeft)
            {
                stoerung += "L";
            }
            else
            {
                stoerung += "R";
            }
        }
        disturb.SetTrigger(stoerung);
    }
}
