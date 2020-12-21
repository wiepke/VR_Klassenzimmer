using System.Collections.Generic;
using UnityEngine;

public static class ImpulseController
{
    private static int levelOfDistortion = 0;
    private static List<GameObject> impulsedStudents = new List<GameObject>();
    private static UnityWaiter waiter;

    public static void SetupImpulseController(UnityWaiter unityWaiter)
    {
        waiter = unityWaiter;
    }

    public static void HandleImpulseNetworkRequest(ImpulseGiven impulseGiven)
    {
        var studentSlots = new List<GameObject>();
        var ids = new HashSet<string>(impulseGiven.students);
        foreach (GameObject studentSlot in AllStudentAttributes.allStudentSlots)
        {
            if (ids.Count == 0)
            {
                if (impulseGiven.impulse == "evoke")
                {
                    if (impulsedStudents.Count == 0)
                    {
                        Debug.LogWarning("es wurde noch kein Impuls gesetzt");
                        return;
                    }
                    EvokeImpulse(impulsedStudents[0]);
                }
                else
                {
                    HandleImpulseWithRandomStudents(impulseGiven.impulse);
                }
                return;
            }
            else
            {

                var sc = studentSlot.GetComponent<StudentController>();
                if (ids.Contains(sc.Id))
                {
                    if (impulseGiven.impulse != "evoke")
                    {
                        studentSlots.Add(studentSlot);
                    }
                    else
                    {
                        EvokeImpulse(studentSlot);
                        return;
                    }
                }
            }
        }
        HandleImpulse(studentSlots, impulseGiven.impulse);
    }

    public static void HandleImpulseWithRandomStudents(string impulse, int n = 4)
    {
        List<GameObject> randomStudentSlots = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            randomStudentSlots.Add(ClassController.GetRandomStudent());
        }
        HandleImpulse(randomStudentSlots, impulse);
    }

    public static void HandleImpulse(List<GameObject> students, string impulse)
    {
        GraphToTreeConverter.StructureTreeNode nextNode;
        switch (impulse)
        {
            case "none":
                nextNode = StructureTreeHandler.GetNextNode(ImpulseType.None);
                break;
            case "positive":
                nextNode = StructureTreeHandler.GetNextNode(ImpulseType.Positive);
                break;
            case "neutral":
                nextNode = StructureTreeHandler.GetNextNode(ImpulseType.Neutral);
                break;
            case "negative":
                nextNode = StructureTreeHandler.GetNextNode(ImpulseType.Negative);
                break;
            default:
                Debug.LogWarning("unknown Impulse: " + impulse);
                return;
        }

        if (nextNode.nodeId != null && nextNode.nextNodes.Count == 0 && impulse == "negative")
        {
            //Desaster is triggered
            MenuDataHolder.isNonScripted = true;
            return;
        }

        if (StructureTreeHandler.stc[StructureTreeHandler.currentNode].soundFileName == "Stö")
        {
            ClassController.SetRandomDisturbance(levelOfDistortion);
            levelOfDistortion++;

            if (nextNode.nextNodes[0].impulseType == ImpulseType.None)
            {
                HandleImpulse(students, "none");
            }
        }
        else if (StructureTreeHandler.stc[StructureTreeHandler.currentNode].soundFileName == "Int")
        {
            ClassController.SolveRandomDisturbance();

            if (nextNode.nextNodes[0].impulseType == ImpulseType.None)
            {
                HandleImpulse(students, "none");
            }
        }
        else if (nextNode.nodeId != null)
        {
            PrepareImpulse(students, impulse, nextNode);
        }
    }

    public static void PrepareImpulse(List<GameObject> studentSlots, string impulse, GraphToTreeConverter.StructureTreeNode nextNode)
    {
        foreach (GameObject studentSlot in studentSlots)
        {
            impulsedStudents.Add(studentSlot);
            studentSlot.GetComponent<BehaviourController>().HandleBehaviour("RaiseArm");
        }
    }

    public static void EvokeImpulse(GameObject studentSlot)
    {
        if (StructureTreeHandler.currentNode != null && studentSlot != null)
        {
            foreach (GameObject impulsedStudent in impulsedStudents)
            {
                if (impulsedStudent != studentSlot)
                {
                    BehaviourController.Disrupt(impulsedStudent, "Idle");
                    impulsedStudents = new List<GameObject>();
                }
            }
            string pathOfAudioFile = StructureTreeHandler.GetPathOfAudioFile(StructureTreeHandler.stc[StructureTreeHandler.currentNode], studentSlot);
            if (pathOfAudioFile != "")
            {
                {
                    BehaviourController bc = studentSlot.GetComponent<BehaviourController>();

                    //there should be an AudioClip, so we look for it
                    waiter.StartCoroutine(waiter.GetAudioClip(pathOfAudioFile, studentSlot, noImpulseNeeded => {
                        if (noImpulseNeeded != null) // Note: "!= null" is true after the coroutine finished. do not remove
                        {
                            bc.HandleBehaviour("Idle");
                            //if there is another student reaction, it will be triggered after "none"-impulse
                            HandleImpulseWithRandomStudents("none");
                        }
                    }));

                    bc.ik.TurnTo(AllStudentAttributes.Teacher.transform);
                    bc.PlayImpulse();
                }
            }

            Debug.Log("You are now at node " + StructureTreeHandler.stc[StructureTreeHandler.currentNode].soundFileName);
        }
    }

}
