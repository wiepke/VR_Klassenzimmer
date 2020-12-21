using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

abstract public class Scatterer : MonoBehaviour
{
    abstract public void Generate(int number);

    public int NumScatter = 0;

    virtual public void Remove()
    {
        List<GameObject> toClear = new List<GameObject>();

        foreach (Transform child in transform)
        {
            toClear.Add(child.gameObject);
        }

        toClear.ForEach((child) => DestroyImmediate(child));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Scatterer), true), CanEditMultipleObjects]
public class ScattererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Scatter"))
        {
            var t = target as Scatterer;
            t.Remove();
            t.Generate(t.NumScatter);
        }
    }
}
#endif