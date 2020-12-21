using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If an object with an AudioSource have a CCSource, it will be tracked by the CCManager and its current playing clip
/// will be lookup on the current CCManager's CCDatabase so the right line can be displayed for the current play time
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class CCSource : MonoBehaviour
{
    public CCCanvas CanvasPrefab;
    
    public bool AlwaysTracked;
    public float MaxDistance = 4.0f;
    public float Scale = 1.0f;

    public bool Displayed => m_Displayed;
    public bool IsPlaying => m_Source.isPlaying;

    AudioSource m_Source;
    bool m_Displayed = false;
    CCCanvas m_Canvas;

    void Start()
    {
        m_Source = GetComponentInChildren<AudioSource>();
        m_Canvas = Instantiate(CanvasPrefab, transform, false);
        m_Canvas.transform.localPosition = Vector3.zero;
        
        Hide();
    }

    void OnEnable()
    {
        CCManager.RegisterSource(this);
    }

    void OnDisable()
    {
        CCManager.RemoveSource(this);
    }

    public void Display(Vector3 toCamera, CCDatabase database)
    {
        if (m_Source.clip == null)
            return;
        
        if (!m_Displayed)
        {
            m_Displayed = true;
            m_Canvas.gameObject.SetActive(true);
        }

        m_Canvas.transform.forward = toCamera;

        string entry = database.GetTextEntry(m_Source.clip, m_Source.time);
        m_Canvas.CCText.text = entry;
    }

    public void Hide()
    {
        m_Displayed = false;
        m_Canvas.gameObject.SetActive(false);
    }

    public void SetLine(string line)
    {
        m_Canvas.CCText.text = line;
    }
}
