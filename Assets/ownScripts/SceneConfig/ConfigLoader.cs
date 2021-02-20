using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class ConfigLoader
{
    public static List<GameObject> posters = new List<GameObject>();

    static string folderPath;

    static Transform lessonDraftSheet;

    static string[] filePath;
    public static void SetupPosters()
    {
        var p = GameObject.FindGameObjectsWithTag("Poster");

        if (p.Length > 0) {
            Transform posterContainer = p[0].transform;
            for (int i=0; i < posterContainer.childCount; i++)
            {
                posters.Add(posterContainer.GetChild(i).gameObject);
            }
        }
    }

    public static void SetupLessonDraft()
    {
        lessonDraftSheet = GameObject.Find("LessonDraft").transform;

    }

    public static void HandleThemeChange(string fromFolder)
    {
        LoadPosters(fromFolder);
        LoadLessonDraft(fromFolder);
        lessonDraftSheet.gameObject.SetActive(true);
        LoadStructureTree(fromFolder);
        Debug.Log("Theme " + fromFolder + " requested");
    }

    private static void LoadPosters(string fromFolder)
    {
        Debug.Log("Es gibt " + posters.Count + " Poster in der Szene");
        folderPath = Application.streamingAssetsPath + "/Config/" + fromFolder + "/posterContent/";
        filePath = Directory.GetFiles(folderPath, "*.jpg");
        for (int i = 0; i < posters.Count; i++)
        {
            byte[] pngBytes = File.ReadAllBytes(filePath[i % filePath.Length]);

            var tex = new Texture2D(2, 2);
            tex.LoadImage(pngBytes);

            var renderer = posters[i].GetComponent<MeshRenderer>();
            renderer.material.SetTexture("_BaseMap", tex);
            //posters[i].SetActive(true); //is handled when animation is triggered
        }
    }

    public static void LoadLessonDraft(string fromFolder)
    {
        folderPath = Application.streamingAssetsPath + "/Config/" + fromFolder + "/LessonDraft/";
        filePath = Directory.GetFiles(folderPath, "*.jpg");
        byte[] pictureBytes = File.ReadAllBytes(filePath[0]);

        var tex = new Texture2D(2, 2);
        tex.LoadImage(pictureBytes);

        var renderer = lessonDraftSheet.GetComponent<MeshRenderer>();
        renderer.material.SetTexture("_BaseMap", tex);
    }

    private static void LoadStructureTree(string fromFolder)
    {
        StructureTreeHandler.LoadStructureTree(fromFolder);
    }
}
