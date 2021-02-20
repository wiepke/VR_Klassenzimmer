using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;

public class ReadCSVFile : MonoBehaviour {

    private readonly List<CsvTitles> disturbances = new List<CsvTitles>();
    private int distCounter = 0;

    void Start()
    {
        if (MenuDataHolder.isExperiment)
        {
            // load lesson draft sheet
            ConfigLoader.SetupLessonDraft();
            ConfigLoader.LoadLessonDraft("Experiment");
           
            string[] data = null;

            if (MenuDataHolder.NumberOfExperiment == 1) {
                TextAsset automatedDistortions = null;
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
                data = automatedDistortions.text.Split(new char[] { '\n' });
            }
            else if (MenuDataHolder.NumberOfExperiment == 2)
            {
                string text = null;
                string filePath = Application.streamingAssetsPath + "/Config/Experiment/automatedDistortions2.csv";
                if (File.Exists(filePath))
                {
                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                    var read = new StreamReader(fileStream);
                    text = read.ReadToEnd();
                    data = text.Split(new char[] { '\n' });
                }
                else
                {
                    throw (new IOException("No file found. "+ filePath));
                }
            }
            if(data != null)
            {
                for (int i = 1; i < data.Length - 1; i++)
                {
                    string[] row = data[i].Split(new char[] { ',' });
                    var disturbance = new CsvTitles();
                    int.TryParse(row[0], out disturbance.id);
                    int.TryParse(row[1], out disturbance.timeInS);
                    disturbance.studentPlace = row[2];
                    disturbance.distortion = row[3];
                    disturbances.Add(disturbance);
                }

                disturbances.Sort(SortByTime);

                foreach (CsvTitles disturbance in disturbances)
                {
                    Invoke("triggerDisturbance", disturbance.timeInS);
                }
            }
            else
            {
                throw (new IOException("No expirement number selected."));
            }
        }
	}

    void triggerDisturbance()
    {
        string stoerung = disturbances[distCounter].distortion;
        string studentPlace = disturbances[distCounter].studentPlace;
        distCounter++;
        GameObject student = GetStudent(studentPlace);
        ClassController.DisruptStudent(student, stoerung);
    }

    static int SortByTime(CsvTitles p1, CsvTitles p2)
    {
        return p1.timeInS.CompareTo(p2.timeInS);
    }

    private GameObject GetStudent(string studentPlace)
    {
        try
        {
            return GameObject.Find("classroom-scaler/Students/" + studentPlace).gameObject;
        }
        catch (System.Exception)
        {
            Debug.Log("student " + studentPlace + " not found");
            return null;
        }
    }
}
