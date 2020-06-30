using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    protected Animator Animator;
    public bool Active = true;

    public float IKIntensity = 0.8f;

    public static Transform Target { get; set; }

    private StudentController sc;

    private void Start()
    {
        sc = GetComponent<StudentController>();

        if (Target == null)
            Target = GameObject.FindGameObjectWithTag("Teacher").GetComponent<TeacherController>().LookAtTarget;
    }

    void OnAnimatorIK()
    {
        if (Active)
        {
            
        }
    }

    private void Update()
    {
        if (Active)
        {
            Active = true;

            
        }
    }
}
