using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MenuMethods
{

    public static void Save()
    {
        List<string[]> rowData = new List<string[]>();

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[3];
        rowDataTemp[0] = "DataCount";
        rowDataTemp[1] = "PositionX";
        rowDataTemp[2] = "PositionY";
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        for (int i = 0; i < MenuDataHolder.evaluationMap.Count - 1; i++)
        {
            rowDataTemp = new string[3];
            rowDataTemp[0] = "" + i; // DataCount
            rowDataTemp[1] = "" + MenuDataHolder.evaluationMap[i].x; // PositionX
            rowDataTemp[2] = "" + MenuDataHolder.evaluationMap[i].y; // PositionY
            rowData.Add(rowDataTemp);

            MenuDataHolder.walkedDistance += Vector2.Distance(MenuDataHolder.evaluationMap[i], MenuDataHolder.evaluationMap[i + 1]);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    private static string getPath()
    {
        #if UNITY_EDITOR
        return Application.dataPath + "/Student_Movement/" + "Student" + MenuDataHolder.repetitionCount + ".csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
        #else
        return Application.dataPath +"/"+"Saved_data.csv";
        #endif
    }
}
