using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Generates new randomly assembled students.
/// </summary>
public class RandomizedStudents : StudentPlacer
{
    /// <summary>
    /// List of students to spread over classroom.
    /// </summary>
    public Student[] Students;

    public int Seed = 0;

    private System.Random rng;
    private Stack<Student> studentStack; // will be used as stack to pull students from
    
    /// <summary>
    /// Prepare randomization by shuffling students into a stack.
    /// </summary>
    public override void InitPlacer()
    {
        // For student distribution, give option of seeded random number generation
        rng = Seed != 0 ? new System.Random(Seed) : new System.Random();
        studentStack = new Stack<Student>();
        var remaining = new List<Student>(Students);

        while (remaining.Count > 0)
        {
            int next = rng.Next(remaining.Count - 1);
            studentStack.Push(remaining[next]);
            remaining.RemoveAt(next);
        }
    }

    public override Student GetNextStudent()
    {
        return studentStack.Pop();
    }
}
