using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Simple helper function that copy template scenes to simplify creating new scenes with the relevant objects.
/// </summary>
public class SceneCreator : MonoBehaviour
{
    [MenuItem("VR Beginner/Create new Prototype Scene")]
    static void CreateSceneRoom()
    {
        NewScene("EmptyPrototypeScene");
    }
    
    [MenuItem("VR Beginner/Create new Empty Scene")]
    static void CreateSceneEmpty()
    {
        NewScene("EmptyScene");
    }

    static void NewScene(string originalScene)
    {
        if (!Directory.Exists(Application.dataPath + "/MyScenes"))
        {
            Directory.CreateDirectory(Application.dataPath + "/MyScenes");
        }
        
        string originalPath = "Assets/Scenes/Template/"+originalScene;
        string newPath = EditorUtility.SaveFilePanel("New Scene", "Assets/MyScenes", "MyScene", "unity");

        if (!string.IsNullOrEmpty(newPath))
        {
            newPath = newPath.Replace(Application.dataPath, "Assets");
            
            //if return false, maybe they moved the original asset, try to search for it
            if (!AssetDatabase.CopyAsset(originalPath, newPath))
            {
                string[] foundAssets = AssetDatabase.FindAssets($"{originalScene} t:Scene");

                if (foundAssets.Length == 0)
                {
                    Debug.LogError("Couldn't find the Template scene. Did you delete it?");
                }
                else
                {
                    originalPath = AssetDatabase.GUIDToAssetPath(foundAssets[0]);
                    if (!AssetDatabase.CopyAsset(originalPath, newPath))
                    {
                        Debug.LogErrorFormat("Couldn't copy Template scene  at {0}", originalPath);
                    }
                    else
                    {
                        EditorSceneManager.OpenScene(newPath);
                    }
                }
            }
        }
    }
}
