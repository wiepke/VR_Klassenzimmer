using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControlMenu : MonoBehaviour
{
    public GameObject studentCount;
    public GameObject sliderStudentCount;
    public GameObject misbehaviorLevel;
    public GameObject sliderMisbehaviorCount;
    public GameObject togglePresentation;
    public GameObject toggleAutomaticIntervention;
    public GameObject expDropdown;
    public GameObject evaluationWalkingBackground;
    private Texture[] textures = new Texture[2];
    public GameObject evalDistance;
    public GameObject evalMisbCount;
    public GameObject evalMisbSolved;


    public void Start()
    {
        textures[0] = Resources.Load<Texture>("texture/FrontalSceneEvaluationBackground");
        textures[1] = Resources.Load<Texture>("texture/GroupSceneEvaluationBackground");
        if (MenuDataHolder.ChosenScene > 0)
        {
            evaluationWalkingBackground.GetComponent<Renderer>().material.mainTexture = textures[MenuDataHolder.ChosenScene - 1];
        }
        evalDistance.GetComponent<TextMeshProUGUI>().text = MenuDataHolder.walkedDistance.ToString();
        evalMisbCount.GetComponent<TextMeshProUGUI>().text = MenuDataHolder.MisbehaviourCount.ToString();
        evalMisbSolved.GetComponent<TextMeshProUGUI>().text = MenuDataHolder.MisbehaviourSolved.ToString();
    }
    public void loadFrontalScene()
    {
        MenuDataHolder.ChosenScene = 1;
        MenuDataHolder.repetitionCount++;
        MenuDataHolder.walkedDistance = 0;
        MenuDataHolder.MisbehaviourCount = 0;
        MenuDataHolder.MisbehaviourSolved = 0;
        SceneManager.LoadScene(1);
    }
    public void loadGroupScene()
    {
        MenuDataHolder.repetitionCount++;
        MenuDataHolder.ChosenScene = 2;
        MenuDataHolder.walkedDistance = 0;
        MenuDataHolder.MisbehaviourCount = 0;
        MenuDataHolder.MisbehaviourSolved = 0;
        SceneManager.LoadScene(2);
    }

    public void updateStudentCount()
    {
        int newCount = Mathf.RoundToInt(sliderStudentCount.GetComponent<Slider>().value);
        MenuDataHolder.StudentCount = newCount;
        studentCount.GetComponent< TextMeshProUGUI > ().text = newCount.ToString();
    }

    public void updateMisbehaviorLevel()
    {
        int newLevel = Mathf.RoundToInt(sliderMisbehaviorCount.GetComponent<Slider>().value);
        MenuDataHolder.LevelOfMisbehavior = newLevel;
        string newLevelString = "";
        switch (newLevel)
        {
            case 0:
                {
                    newLevelString = "keine";
                    break;
                }
            case 1:
                {
                    newLevelString = "niedrig";
                    break;
                }
            case 2:
                {
                    newLevelString = "medium";
                    break;
                }
            case 3:
                {
                    newLevelString = "hoch";
                    break;
                }
        }
        misbehaviorLevel.GetComponent<TextMeshProUGUI>().text = newLevelString;
    }

    public void setPresentation()
    {
        MenuDataHolder.isPresentation = togglePresentation.GetComponent<Toggle>().isOn;
    }

    public void setAutomaticIntervention()
    {
        MenuDataHolder.isAutomaticIntervention = toggleAutomaticIntervention.GetComponent<Toggle>().isOn;
    }

    public void startExperiment()
    {
        switch (expDropdown.GetComponent<TMP_Dropdown>().value)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    MenuDataHolder.ChosenScene = 1;
                    MenuDataHolder.LevelOfMisbehavior = 1;
                    MenuDataHolder.StudentCount = 30;
                    MenuDataHolder.isPresentation = true;
                    MenuDataHolder.isExperiment = true; 
                    SceneManager.LoadScene(1);
                    break;
                }
            case 2:
                {
                    MenuDataHolder.ChosenScene = 1;
                    MenuDataHolder.LevelOfMisbehavior = 1;
                    MenuDataHolder.StudentCount = 30;
                    MenuDataHolder.isPresentation = false;
                    MenuDataHolder.isExperiment = true;
                    SceneManager.LoadScene(1);
                    break;
                }
            case 3:
                {
                    MenuDataHolder.ChosenScene = 1;
                    MenuDataHolder.LevelOfMisbehavior = 3;
                    MenuDataHolder.StudentCount = 30;
                    MenuDataHolder.isPresentation = true;
                    MenuDataHolder.isExperiment = true;
                    SceneManager.LoadScene(1);
                    break;
                }
            case 4:
                {
                    MenuDataHolder.ChosenScene = 1;
                    MenuDataHolder.LevelOfMisbehavior = 3;
                    MenuDataHolder.StudentCount = 30;
                    MenuDataHolder.isPresentation = false;
                    MenuDataHolder.isExperiment = true;
                    SceneManager.LoadScene(1);
                    break;
                }
        }
        Debug.Log("chosen option: " + expDropdown.GetComponent<TMP_Dropdown>().value);
    }

    public void closeGame()
    {
        Application.Quit();
    }

}
