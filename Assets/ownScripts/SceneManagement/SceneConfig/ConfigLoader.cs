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

    public static void HandleThemeChange(string fromFolder, UnityWaiter waiter)
    {
        LoadPosters(fromFolder);
        LoadLessonDraft(fromFolder);
        lessonDraftSheet.gameObject.SetActive(true);
        LoadStructureTree(fromFolder);
        PlayIntro(waiter);
        Debug.Log("Theme " + fromFolder + " requested");
    }

    private static void LoadPosters(string fromFolder)
    {
        folderPath = Application.streamingAssetsPath + "/Config~/" + fromFolder + "/posterContent/";
        filePath = Directory.GetFiles(folderPath, "*.jpg");
        for (int i = 0; i < posters.Count; i++)
        {
            byte[] pngBytes = File.ReadAllBytes(filePath[i % filePath.Length]);

            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(pngBytes);

            MeshRenderer renderer = posters[i].GetComponent<MeshRenderer>();
            renderer.material.SetTexture("_BaseMap", tex);
            posters[i].SetActive(true); 
        }
    }

    private static void LoadLessonDraft(string fromFolder)
    {
        folderPath = Application.streamingAssetsPath + "/Config~/" + fromFolder + "/LessonDraft/";
        filePath = Directory.GetFiles(folderPath, "*.jpg");
        byte[] pictureBytes = File.ReadAllBytes(filePath[0]);

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pictureBytes);

        MeshRenderer renderer = lessonDraftSheet.GetComponent<MeshRenderer>();
        renderer.material.SetTexture("_BaseMap", tex);
    }

    private static void LoadStructureTree(string fromFolder)
    {
        StructureTreeHandler.LoadStructureTree(fromFolder);
    }

    private static void PlayIntro(UnityWaiter waiter)
    {
        GameObject ambient = GameObject.Find("ClassroomAtmosphere");
        folderPath = Application.streamingAssetsPath + "/Config~/Intro.mp3";
        waiter.StartCoroutine(waiter.GetAudioClip(folderPath, ambient, AudioClipFinished =>
        {
            if (AudioClipFinished != null)
            {
                ambient.GetComponent<AmbientSound>().PlayDefaultAmbient();
            }
        }));
    }
}
