using System;
using UnityEngine;

[Serializable]
public class StudentRestData
{
    public string name;
    public string id;
    public string behaviour;
    public float x, z;

    public StudentRestData(string name, string id, string behaviour, Vector3 position)
    {
        this.name = name;
        this.id = id;
        this.behaviour = behaviour;
        this.x = position.x;
        this.z = position.z;
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }
}
