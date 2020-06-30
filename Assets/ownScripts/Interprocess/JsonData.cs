using System;
using UnityEngine;

/// <summary>
/// Type ceiling for JSON data // TODO maybe as interface?
/// </summary>
[Serializable]
public class JsonData {
    public virtual string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
