using System.Collections.Generic;
using UnityEngine;

public static class ImpulseController
{
    private static int levelOfDistortion = 0;
    private static List<GameObject> impulsedStudents;
    private static UnityWaiter waiter;

    public static void SetupImpulseController(UnityWaiter unityWaiter)
    {
        waiter = unityWaiter;
    }

    public static void HandleImpulseNetworkRequest(ImpulseGiven impulseGiven)
    {
        var students = new List<GameObject>();
        var ids = new HashSet<string>(impulseGiven.students);

        foreach (GameObject student in AllStudentAttributes.allStudents)
        {
            if (ids.Contains(student.GetComponent<StudentController>().Id))
            {
                if (impulseGiven.impulse != "evoke")
                {
                    students.Add(student);
                }
                else
                {
                    EvokeImpulse(student);
                    return;
                }
            }
        }

        impulsedStudents = students;
        HandleImpulse(students, impulseGiven.impulse);
    }

    public static void HandleImpulseWithRandomStudents(string impulse, int n = 4)
    {
        var randomStudents = new List<GameObject>();
        for (int i = 0; i < n; i++)
        {
            randomStudents.Add(ClassController.GetRandomStudent());
        }
        impulsedStudents = randomStudents;
        HandleImpulse(randomStudents, impulse);
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
                Debug.LogWarning("unknown Impulse");
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

    public static void PrepareImpulse(List<GameObject> students, string impulse, GraphToTreeConverter.StructureTreeNode nextNode)
    {
        foreach (BehaviourController bc in ClassController.Behaviours)
        {
            bc.Disrupt("RaiseArm");
        }
    }

    public static void EvokeImpulse(GameObject student)
    {
        if (StructureTreeHandler.currentNode != null && student != null)
        {
            foreach (GameObject impulsedStudent in impulsedStudents)
            {
                if (impulsedStudent != student)
                {
                    BehaviourController.Disrupt(impulsedStudent, "Idle");
                }
            }
            string pathOfAudioFile = StructureTreeHandler.GetPathOfAudioFile(StructureTreeHandler.stc[StructureTreeHandler.currentNode], student);
            if (pathOfAudioFile != "")
            {
                {
                    BehaviourController bc = student.GetComponent<BehaviourController>();

                    //there should be an AudioClip, so we look for it
                    waiter.StartCoroutine(waiter.GetAudioClip(pathOfAudioFile, student, noImpulseNeeded => {
                        //if (noImpulseNeeded != null) // FIXME: The "!= null" can probably be removed
                        //{
                            bc.Disrupt("Idle");
                            //if there is another student reaction, it will be triggered after "none"-impulse
                            HandleImpulseWithRandomStudents("none");
                        //}
                    }));

                    //look for animations to illustrate talking students and get a random animationclip
                    float randomImpulse = Random.value;

                    student.GetComponent<Animator>().SetFloat("randomImpulse", randomImpulse);
                    ClassController.TurnToTeacher(student);
                    bc.Disrupt("Impulse");
                }
            }

            Debug.Log("You are now at node " + StructureTreeHandler.stc[StructureTreeHandler.currentNode].soundFileName);
        }
    }

}
