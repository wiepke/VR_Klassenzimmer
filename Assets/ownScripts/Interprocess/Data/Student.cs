using System;
using UnityEngine;

[Serializable]
public class Student
{
    public string name;
    public string id;
    public string behaviour;

    public Student(string name, string id, string behaviour)
    {
        this.name = name;
        this.id = id;
        this.behaviour = behaviour;
    }

    public Student(StudentController sc)
    {
        this.name = sc.Name;
        this.id = sc.Id;
        this.behaviour = sc.Behaviour;
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
