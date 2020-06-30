using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassController : MonoBehaviour
{
    public Dictionary<string, BehaviourController> Students;

    void Start()
    {
        Students = new Dictionary<string, BehaviourController>();
        SocketEventHandler handler = GetComponent<SocketEventHandler>();

        BehaviourController.Handler = handler;

        foreach (GameObject s in GameObject.FindGameObjectsWithTag("Student"))
        {
            BehaviourController bc = s.GetComponent<BehaviourController>();
            StudentController sc = s.GetComponent<StudentController>();
            Students.Add(sc.Id, bc);
        }
    }

    public void DisruptClass(string behaviour)
    {
        foreach (string id in Students.Keys) DisruptClass(id, behaviour);
    }

    public void DisruptClass(Disruption d)
    {
        DisruptClass(d.students, d.behaviour);
    }

    public void DisruptClass(string[] ids, string behaviour)
    {
        foreach (string id in ids) DisruptClass(id, behaviour);
    }

    public void DisruptClass(string studentId, string behaviour)
    {
        BehaviourController dc;
        if (!Students.TryGetValue(studentId, out dc))
            throw new KeyNotFoundException("No student registered for key " + studentId);

        dc.DisruptClass(behaviour);
    }
}
