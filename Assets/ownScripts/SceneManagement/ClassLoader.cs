using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ClassLoader : MonoBehaviour
{
    public Transform Classroom;
    public GameObject ChairArrangementPrefab;

    private void Start()
    {
        LoadClassroom();
    }

    public void LoadClassroom()
    {
        GameObject chairs = Classroom.Find("Chairs")?.gameObject;
        if (chairs != null) DestroyImmediate(chairs);

        chairs = Instantiate(ChairArrangementPrefab, Classroom);
        chairs.name = "Chairs";
    }

    public void LoadClassroom(GameObject classroom)
    {
        ChairArrangementPrefab = classroom;
        LoadClassroom();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ClassLoader)), CanEditMultipleObjects]
public class ClassLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Load Classroom"))
        {
            ((ClassLoader)target).LoadClassroom();
        }
    }
}
#endif
