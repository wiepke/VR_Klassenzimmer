﻿using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public static class StructureTreeHandler
{
    public static string currentNode {get; set;}
    public static Dictionary<string, GraphToTreeConverter.StructureTreeNode> stc { get; set; }
    private static string themeFolder;

    public static void LoadStructureTree(string fromFolder)
    {
        string path = Application.streamingAssetsPath + "/Config~/" + fromFolder;
        //Debug.Log(path);
        stc = StructureTreeContainer.Load(path);
        currentNode = GetFirstNode(stc);
        themeFolder = fromFolder;
    }

    public static string GetFirstNode(Dictionary<string, GraphToTreeConverter.StructureTreeNode> stc)
    {
        return "n0"; //hard coded first node of bismarck-tree. Not a good solution at all
    }

    public static string GetPathOfAudioFile(GraphToTreeConverter.StructureTreeNode stn, GameObject student)
    {
        if (stn.soundFileName == "KM" || stn.soundFileName == "")
        {
            return "";
        }
        string path = Application.streamingAssetsPath + 
            "/Config~/" + themeFolder + 
            "/Audioaufnahmen/Ebene" + stn.soundFileName[0] + 
            "/"+ stn.soundFileName;
        if (student.GetComponent<StudentController>().IsMale)
        {
            path = path + "m";
        }
        else
        {
            path = path + "w";
        }
        path = path + "/";
        int rndSound = 0;
        System.Random rnd = new System.Random();
        string[] audioFileNames = Directory.GetFiles(path);
        rndSound = rnd.Next(0, audioFileNames.Length);
        path = audioFileNames[rndSound];
        return path;
    }

    public static GraphToTreeConverter.StructureTreeNode GetNextNode(ImpulseType impulseType)
    {
        GraphToTreeConverter.StructureTreeNode result = new GraphToTreeConverter.StructureTreeNode();
        if (currentNode != null)
        {
            foreach (StructureTreeContainer.NextNode nextNode in stc[currentNode].nextNodes)
            {
                if(nextNode.impulseType == impulseType)
                {
                    result = stc[nextNode.idOfNode];
                }
            }
            if (result.nodeId != null)
            {
                currentNode = result.nodeId;
            }
        }
        else
        {
            Debug.LogError("you didn't load the Structuretree yet");
        }
        return result;
    }
}