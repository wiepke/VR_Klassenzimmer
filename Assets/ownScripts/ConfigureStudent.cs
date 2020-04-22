using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureStudent : MonoBehaviour
{
    [SerializeField]
    private Transform teacher;

    [SerializeField]
    private float teacherPresenceSize = 2.8f;

    private GameObject[] allStudents;

    public Transform LookObj { get => teacher; set => teacher = value; }

    private System.Random rnd = new System.Random();

    private sbyte eyeLid = 0;
    private sbyte opening = 5;

    private int degreeOfTurningStudent = 80;
    public static Dictionary<GameObject, StudentAttributes> studentAttributes { get; set; }

    void Start()
    {
        studentAttributes = new Dictionary<GameObject, StudentAttributes>();
        int missingStudents = 30;
        if (MenuDataHolder.StudentCount != 0)
        {
            missingStudents = MenuDataHolder.StudentCount;
        }

        allStudents = GameObject.FindGameObjectsWithTag("Student");

        placeNStudents(missingStudents);

        
        DateTime localDate = DateTime.Now;
        foreach (GameObject student in allStudents)
        {
            StudentAttributes attributes = new StudentAttributes();
            attributes.LastGoodBehaviour = "breathing";
            attributes.isDistorting = false;
            attributes.WhenToBlink = localDate.Minute * 60 + localDate.Second + rnd.Next(5, 15);
            attributes.TimeDelayToLastMisbehaviour = localDate.Minute * 60 + localDate.Second;
            attributes.ChanceToMisbehave = 0.5f;
            studentAttributes.Add(student, attributes);   

            //Add all scripts to students to give them kinda individual characters
                student.AddComponent(typeof(MixamoAttechment));
                student.AddComponent(typeof(playSound));
                student.AddComponent(typeof(thrower));
                student.AddComponent(typeof(breathingStudent));
                student.AddComponent(typeof(IKControl));
                BoxCollider BC = student.AddComponent(typeof(BoxCollider)) as BoxCollider;
                BC.center = new Vector3(0.0022482f, 0.337685f, 0.0786477f);
                BC.size = new Vector3(0.3283885f, 1.067539f, 0.514855f);
                AudioSource AS = student.AddComponent(typeof(AudioSource)) as AudioSource;
                AS.loop = false;
                AS.playOnAwake = false;
                AS.spatialBlend = 1;
                AS.rolloffMode = AudioRolloffMode.Linear;
                AS.maxDistance = 10;
        }
    }

    void Update()
    {
        automateStudents();
    }

    private void automateStudents()
    {
        foreach (GameObject student in allStudents)
        {
            if(!(studentAttributes[student].isDistorting) && (studentAttributes[student].LastGoodBehaviour == "breathing"))
            {
                student.GetComponent<IKControl>().ikActive = true;
                setLookDirection(student);
            }
            else
            {
                student.GetComponent<IKControl>().ikActive = false;
            }
            

            if (MenuDataHolder.isAutomaticIntervention)
            {
                if(studentAttributes[student].isDistorting)
                {
                    float distance = Vector3.Distance(student.transform.position, teacher.transform.position);
                    //todo: enrich list of standard interventions. First implemented: standing close to a student
                    if (distance <= teacherPresenceSize)
                    {
                        triggerLastGoodBehaviour(student);
                    }
                }
                
            }
        }
    }

    public static void triggerLastGoodBehaviour(GameObject student)
    {
        studentAttributes[student].isDistorting = false;
        MenuDataHolder.MisbehaviourSolved++;
        student.GetComponent<IKControl>().ikActive = true;
        Animator anim = student.GetComponent<Animator>();
        anim.SetTrigger(studentAttributes[student].LastGoodBehaviour);
        studentAttributes[student].TimeDelayToLastMisbehaviour = DateTime.Now.Minute * 60 + DateTime.Now.Second;
    }

    private void setLookDirection(GameObject student)
    {
        Vector3 vectorFromStudentToTeacher = student.transform.position - teacher.transform.position;
        Vector3 studentViewDirection = student.transform.forward;
        if (Vector3.Angle(studentViewDirection, vectorFromStudentToTeacher) > 100f)
        {
            student.GetComponent<IKControl>().setIkIntensity(1.0f);
        }
        else
        {
            student.GetComponent<IKControl>().setIkIntensity(0.0f);
        }
    }

    private void placeNStudents(int n)
    {
        /*
        //random selection
        for (int i = 0; i < missingStudents; i++)
        {
            int shownStudent = Random.Range(0, allStudents.Length);
            if (allStudents[shownStudent].activeInHierarchy)
            {
                allStudents[shownStudent].SetActive(false);
            }
        }
        */

        //front rows are placed
        foreach (GameObject student in allStudents)
        {
            if (n > 0)
            {
                student.SetActive(true);
            }
            else
            {
                student.SetActive(false);
            }
            n--;
        }
    }

    public GameObject getStudent(string studentPlace)
    {
        try
        {
            return GameObject.Find("classroom-scaler/Students/" + studentPlace).transform.GetChild(0).gameObject;
        }
        catch (System.Exception)
        {
            Debug.Log("student " + studentPlace + " not found");
            return null;
        }
    }
}
