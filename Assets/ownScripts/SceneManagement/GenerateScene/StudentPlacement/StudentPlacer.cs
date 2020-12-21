using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Abstract component to genrate students to be placed in a classroom.
/// </summary>
public abstract class StudentPlacer : MonoBehaviour
{
    public float MaxPartnerDistance = 4.0f;

    public abstract void InitPlacer();

    public abstract Student GetNextStudent();

    /// <summary>
    /// Used by editor to update component-specific information.
    /// </summary>
    public virtual void EditorUpdateHook() { }
}

#if UNITY_EDITOR
[CustomEditor(typeof(StudentPlacer), true)]
public class StudentPlacerEditor : Editor
{
    int placedStudents = 0;
    public override void OnInspectorGUI()
    {
        StudentPlacer sp = (StudentPlacer)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Place Students"))
        {
            sp.InitPlacer();
            var studentSlots = GameObject.FindGameObjectsWithTag("StudentSlot");
            PlaceStudents(sp, studentSlots);
        }

        if (placedStudents > 0)
        {
            GUILayout.Label("Placed " + placedStudents + " Students!");
        }
    }

    public void SpawnStudent(StudentController sc)
    {
        var student = sc.StudentObj;

        string modelName = "Students/" + (student.IsMale ? "Male" : "Female") + "/" + student.ModelName; 
        var Model = Resources.Load<GameObject>(modelName);

        // Destroy previous student object
        if (sc.transform.childCount > 0)
            DestroyImmediate(sc.transform.GetChild(0).gameObject);

        sc.Model = Instantiate(Model, sc.transform);
    }

    private void PlaceStudents(StudentPlacer sp, GameObject[] slots)
    {
        foreach (var slot in slots)
        {
            placedStudents++;

            var sc = slot.GetComponent<StudentController>();
            sc.StudentObj = sp.GetNextStudent();
            SpawnStudent(sc);
        }
    }
}
#endif