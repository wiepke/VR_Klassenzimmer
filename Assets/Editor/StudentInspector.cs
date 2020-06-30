using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StudentController))]
public class StudentInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        StudentController student = (StudentController)target;
    }
}
