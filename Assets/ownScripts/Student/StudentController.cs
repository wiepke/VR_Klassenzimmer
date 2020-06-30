using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Generates and manages student instance logic.
/// </summary>
public class StudentController : MonoBehaviour
{
    /// <summary>
    /// Name of the student.
    /// </summary>
    public string Name;
    public string Id;
    public GameObject Model;
    public bool IsMale;

    public string Behaviour = "idle";

    public Animator StudentAnimator;

    private void Start()
    {
        Behaviour = "idle";
        GameObject go = Instantiate(Model, transform);
        StudentAnimator = go.GetComponent<Animator>();
    }

    public static BootstrapResponse ClassToJson()
    {
        var students = GameObject.FindGameObjectsWithTag("Student");
        var res = new Student[students.Length];
        var i = 0;

        foreach (var student in students)
        {
            var sc = student.GetComponent<StudentController>();
            res[i] = new Student(sc);
            i++;
        }

        return new BootstrapResponse(res);
    }
}
