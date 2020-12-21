using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;

public class readCSVFile : MonoBehaviour {

    List<csvTitles> disturbances = new List<csvTitles>();
    int distCounter = 0;

    string folderPath;
    string[] file;
    static Transform lessonDraftSheet;

    // Use this for initialization
    void Start()
    {
        if (MenuDataHolder.isExperiment)
        {
            // load lesson draft sheet
            lessonDraftSheet = GameObject.Find("LessonDraft").transform;
            folderPath = Application.streamingAssetsPath + "/Config/Experiment/";
            file = Directory.GetFiles(folderPath, "*.jpg");

            byte[] pictureBytes = File.ReadAllBytes(file[0]);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(pictureBytes);

            MeshRenderer renderer = lessonDraftSheet.GetComponent<MeshRenderer>();
            renderer.material.SetTexture("_BaseMap", tex);
        }

        TextAsset automatedDistortions = null;

        if (MenuDataHolder.NumberOfExperiment == 1) {
            if (MenuDataHolder.isExperiment)
            {
                if (MenuDataHolder.isPresentation)
                {
                    switch (MenuDataHolder.LevelOfMisbehavior)
                    {
                        case 0: //lecture few-many, monitoring few-many (l1m1)
                            {
                                automatedDistortions = Resources.Load<TextAsset>("l1m1"); // currently the only one which is working, others have to be changed
                                break;
                            }
                        case 1: // lecture many-few, monitoring many-few (l2m2)
                            {
                                automatedDistortions = Resources.Load<TextAsset>("l2m2");
                                break;
                            }
                        case 2: // lecture few-many, montoring many-few (l1m2)
                            {
                                automatedDistortions = Resources.Load<TextAsset>("l1m2");
                                break;
                            }
                        case 3: // lecture many-few, monitoring few-many (l2m1)
                            {
                                automatedDistortions = Resources.Load<TextAsset>("l2m1");
                                break;
                            }
                        default:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedPresentationNone");
                                break;
                            }
                    }
                }
                else
                {
                    switch (MenuDataHolder.LevelOfMisbehavior)
                    {
                        case 0:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("m1l1");
                                break;
                            }
                        case 1:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("m2l2");
                                break;
                            }
                        case 2:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("m1l2");
                                break;
                            }
                        case 3:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("m2l1");
                                break;
                            }
                        default:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedSuperviceNone");
                                break;
                            }
                    }
                }
            }
            else
            {
                if (MenuDataHolder.isPresentation)
                {
                    switch (MenuDataHolder.LevelOfMisbehavior)
                    {
                        case 1:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedPresentationLow");
                                break;
                            }
                        case 2:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedPresentationMedium");
                                break;
                            }
                        case 3:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedPresentationHigh");
                                break;
                            }
                        default:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedPresentationNone");
                                break;
                            }
                    }
                }
                else
                {
                    switch (MenuDataHolder.LevelOfMisbehavior)
                    {
                        case 1:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedSuperviceLow");
                                break;
                            }
                        case 2:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedSuperviceMedium");
                                break;
                            }
                        case 3:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedSuperviceHigh");
                                break;
                            }
                        default:
                            {
                                automatedDistortions = Resources.Load<TextAsset>("automatedSuperviceNone");
                                break;
                            }
                    }
                }
            }
        }
        else if (MenuDataHolder.NumberOfExperiment == 2)
        {
            automatedDistortions = Resources.Load<TextAsset>("automatedDistortions2");
        }
        else
        {
            automatedDistortions = Resources.Load<TextAsset>("automatedPresentationNone");
        }

        string[] data = automatedDistortions.text.Split(new char[] { '\n' });
        for (int i=1; i<data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            csvTitles disturbance = new csvTitles();
            int.TryParse(row[0], out disturbance.id);
            int.TryParse(row[1], out disturbance.timeInS);
            disturbance.studentPlace = row[2];
            disturbance.distortion = row[3];
            disturbances.Add(disturbance);
        }
        disturbances.Sort(SortByTime);
        foreach (csvTitles disturbance in disturbances)
        {
            Invoke("triggerDisturbance", disturbance.timeInS);
        }
	}

    void triggerDisturbance()
    {
        string stoerung = disturbances[distCounter].distortion;
        string studentPlace = disturbances[distCounter].studentPlace;
        distCounter++;
        GameObject student = getStudent(studentPlace);
        ClassController.DisruptStudent(student, stoerung);
    }

    static int SortByTime(csvTitles p1, csvTitles p2)
    {
        return p1.timeInS.CompareTo(p2.timeInS);
    }

    private GameObject getStudent(string studentPlace)
    {
        try
        {
            return GameObject.Find("classroom-scaler/Students/" + studentPlace);
        }
        catch (System.Exception)
        {
            Debug.Log("student " + studentPlace + " not found");
            return null;
        }
    }
}
