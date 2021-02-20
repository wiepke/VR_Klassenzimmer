﻿using System.Collections;
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
    public GameObject evalMisbSeen;
    public GameObject toggleNonScriptedBehaviour;
    [SerializeField] private GameObject toggleTimer;
    [SerializeField] private GameObject inputFieldTimer;

    private ExperimentParameters[] experiments;

    public void Start()
    {
        textures[0] = Resources.Load<Texture>("texture/FrontalSceneEvaluationBackground");
        textures[1] = Resources.Load<Texture>("texture/GroupSceneEvaluationBackground");

        if (MenuDataHolder.ChosenScene > 0)
        {
            evaluationWalkingBackground.GetComponent<Renderer>().material.mainTexture = textures[MenuDataHolder.ChosenScene - 1];
        }

        evalDistance.GetComponent<TextMeshProUGUI>().text = MenuDataHolder.WalkedDistance.ToString();
        evalMisbCount.GetComponent<TextMeshProUGUI>().text = MenuDataHolder.MisbehaviourCount.ToString();
        evalMisbSolved.GetComponent<TextMeshProUGUI>().text = MenuDataHolder.MisbehaviourSolved.ToString();
        evalMisbSeen.GetComponent<TextMeshProUGUI>().text = MenuDataHolder.MisbehaviourSeen.ToString();

        // Setup basic experiments for selection. (currently) To add new experiments edit this.
        // TODO Find a better way to do this
        experiments = new ExperimentParameters[] {
            new ExperimentParameters(0, 10, true, true, 1),
            new ExperimentParameters(1, 10, true, true, 1),
            new ExperimentParameters(2, 10, true, true, 1),
            new ExperimentParameters(3, 10, true, true, 1),
            new ExperimentParameters(0, 30, true, true, 1),
            new ExperimentParameters(1, 30, true, true, 1),
            new ExperimentParameters(2, 30, true, true, 1),
            new ExperimentParameters(3, 30, true, true, 1),
            new ExperimentParameters(1, 30, true, true, 1),

            new ExperimentParameters(0, 10, false, true, 1),
            new ExperimentParameters(1, 10, false, true, 1),
            new ExperimentParameters(2, 10, false, true, 1),
            new ExperimentParameters(3, 10, false, true, 1),
            new ExperimentParameters(0, 30, false, true, 1),
            new ExperimentParameters(1, 30, false, true, 1),
            new ExperimentParameters(2, 30, false, true, 1),
            new ExperimentParameters(3, 30, false, true, 1),
            new ExperimentParameters(1, 30, false, true, 1),

            new ExperimentParameters(0, 15, true, true, 2),
            new ExperimentParameters(0, 30, true, true, 2)
        };

        //Set timer toggle
        toggleTimer.GetComponent<Toggle>().isOn = MenuDataHolder.TimerActive;
    }

    private void LoadScene(int sceneIndex)
    {
        // Update Data Holder
        MenuDataHolder.ChosenScene = sceneIndex;
        MenuDataHolder.RepetitionCount++;
        MenuDataHolder.WalkedDistance = 0;
        MenuDataHolder.MisbehaviourCount = 0;
        MenuDataHolder.MisbehaviourSolved = 0;
        MenuDataHolder.MisbehaviourSeen = 0;

        SceneManager.LoadScene(sceneIndex);
    }

    // UI Callback
    public void loadFrontalScene()
    {
        LoadScene(1);
    }

    // UI Callback
    public void loadGroupScene()
    {
        LoadScene(2);
    }

    public void updateStudentCount()
    {
        int newCount = Mathf.RoundToInt(sliderStudentCount.GetComponent<Slider>().value);
        MenuDataHolder.StudentCount = newCount;
        studentCount.GetComponent<TextMeshProUGUI>().text = newCount.ToString();
    }

    public void updateMisbehaviorLevel()
    {
        int newLevel = Mathf.RoundToInt(sliderMisbehaviorCount.GetComponent<Slider>().value);
        MenuDataHolder.LevelOfMisbehavior = newLevel;

        string[] level = { "keine", "niedrig", "medium", "hoch" };
        misbehaviorLevel.GetComponent<TextMeshProUGUI>().text = level[newLevel];
    }

    public void setPresentation()
    {
        MenuDataHolder.isPresentation = togglePresentation.GetComponent<Toggle>().isOn;
    }

    public void setAutomaticIntervention()
    {
        MenuDataHolder.isAutomaticIntervention = toggleAutomaticIntervention.GetComponent<Toggle>().isOn;
    }

    public void setNonScriptedBehaviour()
    {
        MenuDataHolder.isNonScripted = toggleNonScriptedBehaviour.GetComponent<Toggle>().isOn;
    }

    public void SetTimer()
    {
        MenuDataHolder.TimerActive = toggleTimer.GetComponent<Toggle>().isOn;
    }

    public void SetTimerSeconds()
    {
        int input;
        if (int.TryParse(inputFieldTimer.GetComponent<TMP_InputField>().text, out input))
        {
            MenuDataHolder.TimerSeconds = input;
        }
    }
    // Reduce code repetition
    private void LoadExperiment(ExperimentParameters experiment)
    {
        MenuDataHolder.ChosenScene = 1;
        MenuDataHolder.LevelOfMisbehavior = experiment.MisbehaviourLevel;
        MenuDataHolder.StudentCount = experiment.StudentCount;
        MenuDataHolder.isPresentation = experiment.IsPresentation;
        MenuDataHolder.isExperiment = experiment.IsExperiment;

        SceneManager.LoadScene(1);
    }

    public void startExperiment()
    {
        var value = expDropdown.GetComponent<TMP_Dropdown>().value;
        if (value > 0) LoadExperiment(experiments[value - 1]);

        Debug.Log("chosen option: " + expDropdown.GetComponent<TMP_Dropdown>().value);
    }

    public void closeGame()
    {
        Application.Quit();
    }

}

/// <summary>
/// Encode experiment parameters in scene selection.
/// </summary>
public struct ExperimentParameters
{
    // NOTE: Please feel free to extend this as needed! This is just a rough draft.

    public readonly int MisbehaviourLevel;
    public readonly int StudentCount;
    public readonly bool IsPresentation;
    public readonly bool IsExperiment;
    public readonly int ExperimentNumber;

    public ExperimentParameters(
        int misbehaviourLevel,
        int studentCount,
        bool presentation,
        bool experiment,
        int experimentNumber)
    {
        MisbehaviourLevel = misbehaviourLevel;
        StudentCount = studentCount;
        IsPresentation = presentation;
        IsExperiment = experiment;
        ExperimentNumber = experimentNumber;
    }
}
