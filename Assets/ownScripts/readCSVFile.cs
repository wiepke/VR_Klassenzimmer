using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class readCSVFile : MonoBehaviour {

    List<csvTitles> disturbances = new List<csvTitles>();
    int distCounter = 0;
    DistortionTrigger trigger;
    ConfigureStudent cs;

    // Use this for initialization
    void Start ()
    {
        cs = GameObject.Find("Students").GetComponent<ConfigureStudent>();
        trigger = gameObject.AddComponent<DistortionTrigger>();
        TextAsset automatedDistortions;
        if (MenuDataHolder.isExperiment)
        {
            if (MenuDataHolder.isPresentation)
            {
                switch (MenuDataHolder.LevelOfMisbehavior)
                {
                    case 1:
                        {
                            automatedDistortions = Resources.Load<TextAsset>("few_ins_moni");
                            break;
                        }
                    case 3:
                        {
                            automatedDistortions = Resources.Load<TextAsset>("many_ins_moni");
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
                            automatedDistortions = Resources.Load<TextAsset>("few_moni_ins");
                            break;
                        }
                    case 3:
                        {
                            automatedDistortions = Resources.Load<TextAsset>("many_moni_ins");
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
        GameObject student = cs.getStudent(studentPlace);
        trigger.SetDisturbance(student, studentPlace, stoerung);
        distCounter++;
    }

    static int SortByTime(csvTitles p1, csvTitles p2)
    {
        return p1.timeInS.CompareTo(p2.timeInS);
    }
}
