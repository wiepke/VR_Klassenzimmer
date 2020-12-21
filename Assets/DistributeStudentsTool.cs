using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DistributeStudentsTool : MonoBehaviour 
{
    public Student[] Students;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DistributeStudentsTool)), CanEditMultipleObjects]
public class DistributeStudentsToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Distribute"))
        {
            Scene scene = SceneManager.GetActiveScene();
            var tool = ToNameDict(((DistributeStudentsTool)target).Students);
            LoadStudents(tool, Seating());
        }
    }

    public void LoadStudents(Dictionary<string, Student> students, Dictionary<string, string> seatDict)
    {
        var slots = GameObject.FindGameObjectsWithTag("StudentSlot");
        foreach (var s in slots) 
        {
            var sc = s.GetComponent<StudentController>();
            sc.StudentObj = students[seatDict[s.name]];
            sc.Id = s.name;
        }
    }

    public Dictionary<string, Student> ToNameDict(Student[] students)
    {
        var dict = new Dictionary<string, Student>();
        foreach (var s in students) dict.Add(s.Name, s);
        return dict;
    }

    public Dictionary<string, string> Seating()
    {
        var dict = new Dictionary<string, string>()
        {
            { "13L", "Pascal" }, { "13R", "Nina" }, { "14L", "Finn" }, { "14R", "Jasmin" }, { "15L", "Mara" }, { "15R", "Sven" },
            { "10L", "Bea" }, { "10R", "Jessi" }, { "11L", "Florian" }, { "11R", "Elli" }, { "12L", "Harley" }, { "12R", "Amy" },
            { "07L", "Daniel" }, { "07R", "Leni" }, { "08L", "Hannah" }, { "08R", "Bob" }, { "09L", "Sandra" }, { "09R", "Jonas" },
            { "04L", "Moritz" }, { "04R", "Anna" }, { "05L", "Steve" }, { "05R", "Lea" }, { "06L", "Ellias" }, { "06R", "Bennet" },
            { "01L", "Tina" }, { "01R", "Steffi" }, { "02L", "Anton" }, { "02R", "Haralf" }, { "03L", "George" }, { "03R", "Arthur" },
        };

        return dict;
    }
}
#endif