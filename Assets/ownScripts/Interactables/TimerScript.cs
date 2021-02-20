using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject timerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (MenuDataHolder.TimerActive)
        {
            button.SetActive(true);
            canvas.SetActive(true);
            timerPrefab.SetActive(true);
        }
    }
}
