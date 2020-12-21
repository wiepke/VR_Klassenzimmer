using UnityEngine;
using System;

[Serializable]
public class BootstrapResponse : JsonData
{
    public StudentRestData[] students;

    public BootstrapResponse(StudentRestData[] students)
    {
        this.students = students;
    }
}
