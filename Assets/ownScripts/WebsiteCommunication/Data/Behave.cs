using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behave : JsonData
{
    public string id;
    public string behaviour;

    public Behave(string id, string behaviour)
    {
        this.id = id; this.behaviour = behaviour;
    }
}
