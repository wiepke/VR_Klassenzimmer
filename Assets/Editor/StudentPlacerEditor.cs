using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Generates students for all available Attachment points.
/// </summary>
[CustomEditor(typeof(StudentPlacer), true)]
public class StudentPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Place Students"))
        {
            InitializeStudents();
        }
    }

    /// <summary>
    /// Generates a student for every attachment point.
    /// </summary>
    public void InitializeStudents()
    {
        StudentPlacer sp = (StudentPlacer)target;
        sp.EditorUpdateHook();
        int id = 0;

        foreach (GameObject student in GameObject.FindGameObjectsWithTag("Student"))
        {
            StudentController sc = student.GetComponent<StudentController>();
            sc.Id = "" + id;
            sp.InitializeStudent(sc);
            id++; // Numbering should always be in order of instantiation of the tables
        }

        id = 0;
        foreach (GameObject desk in GameObject.FindGameObjectsWithTag("Desk"))
        {
            desk.GetComponent<DeskManager>().SetStudentIds();
        }
    }
}
