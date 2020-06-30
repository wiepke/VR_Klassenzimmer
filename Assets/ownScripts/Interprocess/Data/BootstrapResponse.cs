using UnityEngine;
using System;

[Serializable]
public class BootstrapResponse : JsonData
{
    public Student[] students;

    public BootstrapResponse(Student[] students)
    {
        this.students = students;
    }
}
