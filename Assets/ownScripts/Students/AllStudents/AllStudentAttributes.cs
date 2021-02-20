using System.Collections.Generic;
using UnityEngine;

public static class AllStudentAttributes
{
    public static GameObject[] allStudents { get; set; }
    public static Transform Teacher { get; set; }
    public const float degreeOfTurningStudent = 100f;

    public static void SetupStudentValues()
    {
        int missingStudents = 30;
        if (MenuDataHolder.StudentCount != 0)
        {
            missingStudents = MenuDataHolder.StudentCount;
        }

        allStudents = PlaceNStudents(missingStudents);
    }

    private static GameObject[] PlaceNStudents(int n)
    {
        GameObject[] placedStudents = GameObject.FindGameObjectsWithTag("StudentSlot");
        var result = new List<GameObject>();

        for (int i = 0; i < n; i++)
        {
            placedStudents[i].SetActive(true);
            result.Add(placedStudents[i]);
        }

        return result.ToArray();
    }

    public static BootstrapResponse ClassToJson()
    {
        var res = new StudentRestData[allStudents.Length];
        int i = 0;

        foreach (var student in allStudents)
        {
            var sc = student.GetComponent<StudentController>();
            var bc = student.GetComponent<BehaviourController>();

            string behaviour = bc.CurrentBehaviour;

            res[i] = new StudentRestData(sc.Name, sc.Id, behaviour, student.transform.position);
            i++;
        }

        return new BootstrapResponse(res);
    }

}
