using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeskPlacer), true)]
public class DeskPlacerEditor : Editor
{
    private int NumberToAdd = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DeskPlacer dp = (DeskPlacer)target;

        //dp.DeskPrefab = (GameObject)EditorGUILayout.ObjectField("DeskPrefab", (Object)dp.DeskPrefab, typeof(GameObject));

        //NumberToAdd = EditorGUILayout.IntField("Number of Desks to add:", NumberToAdd);

        if (GUILayout.Button("Generate Desks"))
        {
            dp.GenerateDesks(dp.NumberToAdd);
        }

        if (GUILayout.Button("Clear Desks"))
        {
            dp.Clean();
        }
    }
}
