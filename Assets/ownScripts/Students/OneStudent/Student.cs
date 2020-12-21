using UnityEngine;

[CreateAssetMenu(fileName = "Student", menuName = "ScriptableObjects/Student", order = 1)]
public class Student : ScriptableObject
{
    public string Name;
    public bool IsMale;
    public bool IsTall;
    public string ModelName;
    public bool IsLeftHanded;
}
