using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Generates new randomly assembled students.
/// </summary>
public class RandomizedStudents : StudentPlacer
{
    /// <summary>
    /// Path to folder with male student models.
    /// </summary>
    public string MaleModelsPath = "Students/Male";

    /// <summary>
    /// Array of model prefabs to use for male students.
    /// </summary>
    private GameObject[] MaleModels;

    /// <summary>
    /// Path to folder with male student models.
    /// </summary>
    public string FemaleModelsPath = "Students/Female";

    /// <summary>
    /// Array of model prefabs to use for female students.
    /// </summary>
    private GameObject[] FemaleModels;

    public string MaleNamesPath = "maleNames.txt";
    
    /// <summary>
    /// Array of potential male names.
    /// </summary>
    private string[] MaleNames;

    public string FemaleNamesPath = "femaleNames.txt";

    /// <summary>
    /// Array of potential female names.
    /// </summary>
    private string[] FemaleNames;

    /// <summary>
    /// Chance of the student being female instead of male.
    /// </summary>
    public float ChanceOfFemale = 0.5f;
    // TODO: Seed for randomization

    /// <summary>
    /// Check this to avoid name repetition.
    /// </summary>
    public bool NoDuplicateNames = false; // TODO implement

    /// <summary>
    /// Randomly generate a new student.
    /// </summary>
    /// <param name="chair">A attachment point to parent the student under.</param>
    /// <returns>Newly generated student instance.</returns>
    public override void InitializeStudent(StudentController sc)
    {
        bool male = Random.value > ChanceOfFemale;
        GameObject[] models = male ? MaleModels : FemaleModels;
        string[] names = male ? MaleNames : FemaleNames;

        sc.IsMale = male;
        sc.Model = models[(int)Random.Range(0, models.Length - 1)];
        sc.Name = names[Random.Range(0, names.Length - 1)];
    }

    private string[] ReadLines(string fileName)
    {
        StreamReader reader = new StreamReader("Assets/Resources/" + fileName);
        string line = reader.ReadLine();
        List<string> lines = new List<string>();
        
        while (line != null)
        {
            lines.Add(line);
            line = reader.ReadLine();
        }

        return lines.ToArray();
    }

    public override void EditorUpdateHook()
    {
        FemaleModels = Resources.LoadAll<GameObject>(FemaleModelsPath);
        MaleModels = Resources.LoadAll<GameObject>(MaleModelsPath);
        FemaleNames = ReadLines(FemaleNamesPath);
        MaleNames = ReadLines(MaleNamesPath);
    }
}
