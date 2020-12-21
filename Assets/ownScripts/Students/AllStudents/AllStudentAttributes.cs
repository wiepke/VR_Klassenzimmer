using System.Collections.Generic;
using UnityEngine;

public static class AllStudentAttributes
{
    public static GameObject[] allStudentSlots { get; set; }
    public static Transform Teacher { get; set; } //is set in Script CameraSelector in GameObject "Teacher"
    public const float degreeOfTurningStudent = 100f;

    public static void SetupStudentValues()
    {
        int tooManyStudents = 0;
        if (MenuDataHolder.StudentCount != 0)
        {
            tooManyStudents = 30 - MenuDataHolder.StudentCount;
        }

        allStudentSlots = deactivateNStudents(tooManyStudents);
    }

    private static GameObject[] deactivateNStudents(int n)
    {
        List<GameObject> result = new List<GameObject>(GameObject.FindGameObjectsWithTag("StudentSlot"));
        for (int i = 0; i < n; i++)
        {
            result[i].SetActive(false);
            result.Remove(result[i]);
        }

        return result.ToArray();
    }


    //todo: this function probably is of no use anymore and can be deleted
    public static BootstrapResponse ClassToJson()
    {
        StudentRestData[] res = new StudentRestData[allStudentSlots.Length];
        int i = 0;

        foreach (var student in allStudentSlots)
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
