﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public static class ClassController
{
    public static SocketEventHandler Handler;
    public static GameObject chattingStudent;

    private static List<BehaviourController> behaviourControllers;
    public static List<BehaviourController> Behaviours 
    {
        get 
        {
            if (behaviourControllers == null)
            {
                behaviourControllers = new List<BehaviourController>();
                foreach (GameObject s in AllStudentAttributes.allStudents)
                    behaviourControllers.Add(s.GetComponent<BehaviourController>());
            }
            return behaviourControllers;
        }
    }

    // for replays; replaymanager will listen to this event
    public delegate void replayHandler(BehaviourController bc, string performedBehaviour);
    public static event replayHandler onStudentBehaviour;

    public static void DisruptClass(string behaviour)
    {
        foreach (BehaviourController bc in Behaviours)
            bc.Disrupt(behaviour);
    }

    public static void DisruptClass(Disruption d)
    {
        DisruptClass(d.students, d.behaviour);
    }

    public static void DisruptClass(string[] ids, string behaviour)
    {
        foreach (BehaviourController bc in Behaviours)
        {
            var studentIds = new HashSet<string>(ids);
            if (studentIds.Contains(bc.GetComponent<StudentController>().Id))
                DisruptStudent(bc, behaviour);
        }
    }

    // Wrapper around DisruptStudent for the sake of convenience
    private static void DisruptStudent(BehaviourController bc, string behaviour)
    {
        bc.Disrupt(behaviour);
        Debug.Log(bc);
        Debug.Log(behaviour);

        if(onStudentBehaviour != null)
        {
            onStudentBehaviour.Invoke(bc, behaviour);
        }
    }

    public static void DisruptStudent(GameObject student, string behaviour)
    {
        DisruptStudent(student.GetComponent<BehaviourController>(), behaviour);
    }

    public static GameObject GetRandomStudent()
    {
        int randomStudentId = Random.Range(0, AllStudentAttributes.allStudents.Length);
        return AllStudentAttributes.allStudents[randomStudentId];
    }

    public static void SetRandomDisturbance(GameObject student, int level = 0)
    {
        List<string> PossibleDistortions;

        switch (level)
        {
            case 0:
                PossibleDistortions = new List<string>(SpecialBehaviours._level0Distortion);
                break;
            case 1:
                PossibleDistortions = new List<string>(SpecialBehaviours._level1Distortion);
                break;
            case 2:
                PossibleDistortions = new List<string>(SpecialBehaviours._level2Distortion);
                break;
            default:
                PossibleDistortions = new List<string>(SpecialBehaviours._level0Distortion);
                foreach (string distortion in SpecialBehaviours._level1Distortion)
                {
                    PossibleDistortions.Add(distortion);
                }
                foreach (string distortion in SpecialBehaviours._level2Distortion)
                {
                    PossibleDistortions.Add(distortion);
                }
                break;
        }

        int countDistortions = PossibleDistortions.Count;
        int rndDistortion = Random.Range(0, countDistortions);
        string misbehaviour = PossibleDistortions[rndDistortion];
        Debug.Log(misbehaviour);
        DisruptStudent(student, misbehaviour);
    }

    public static void SetRandomDisturbance(int level = 0)
    {
        SetRandomDisturbance(GetRandomStudent(), level);
    }

    public static void SolveRandomDisturbance() // FIXME: Misleading name, as there is not really any kind of randomnes here
    {
        // TODO: Proposal: calculate number of disturbances, generate random number between 0 and disturbance count - 1, toggle that student.
        foreach (BehaviourController bc in Behaviours)
        {
            if (bc.IsDistorting)
            {
                DisruptStudent(bc, bc.LastGoodBehaviour);
                return;
            }
        }
    }

    public static void TurnToTeacher(GameObject student)
    {
        student.GetComponent<IKControl>().TurnTo(AllStudentAttributes.Teacher.transform);
    }
}