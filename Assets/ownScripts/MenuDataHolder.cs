using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuDataHolder
{
    public static int StudentCount { get; set; } = 30;//atm a value between 1 and 30

    public static int ChosenScene { get; set; } = 1; // 1 represents frontalLecture, 2 represents groupworkScene

    public static int LevelOfMisbehavior { get; set; } = 0;//atm 1, 2 or 3 for low, medium or high level of misbehaviors

    public static bool isPresentation { get; set; } = true;//if true students will look at teacher. if false students will write

    public static bool isAutomaticIntervention { get; set; } = true;//if true misbehaviour is interrupted automatically by a close teacher for example

    public static bool isExperiment { get; set; } = false;//if true scripts from erzwis are used. if false I don't know yet

    public static List<Vector2> evaluationMap { get; set; } = new List<Vector2>();

    public static float walkedDistance { get; set; } = 0;

    public static int MisbehaviourCount { get; set; } = 0;

    public static int MisbehaviourSolved { get; set; } = 0;

    public static int MisbehaviourSeen { get; set; } = 0;

    public static int repetitionCount { get; set; } = 0;
}
