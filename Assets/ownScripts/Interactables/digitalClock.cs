using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class DigitalClock : MonoBehaviour
{
    [SerializeField] private GameObject Time;
    [SerializeField] private GameObject Date;

    private TextMeshProUGUI textTime;
    private TextMeshProUGUI textDate;

    private DateTime dateFetcher;

    void Awake()
    {
        dateFetcher = DateTime.Now;
        textTime = Time.GetComponent<TextMeshProUGUI>();
        textDate = Date.GetComponent<TextMeshProUGUI>();
        string day = LeadingZero(dateFetcher.Day);
        string month = LeadingZero(dateFetcher.Month);
        string year = dateFetcher.Year.ToString();
        textDate.text = day + "." + month + "." + year;
    }

    void Update()
    {
        dateFetcher = DateTime.Now;
        string hour = LeadingZero(dateFetcher.Hour);
        string minute = LeadingZero(dateFetcher.Minute);
        textTime.text = hour + ":" + minute;


    }

    private string LeadingZero(int x)
    {
        return x.ToString().PadLeft(2, '0');
    }


}
