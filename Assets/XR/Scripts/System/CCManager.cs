using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The CCManager will track each CCSource in the scene and hide/display/align them based on the view direction and
/// distance.
/// </summary>
[DefaultExecutionOrder(-999)]
public class CCManager : MonoBehaviour
{
    static CCManager s_Instance;
    public static CCManager Instance => s_Instance;

    public CCDatabase Database;
    public Canvas IndicatorCanvas;
    public GameObject IndicatorPrefab;

    List<CCSource> m_Sources = new List<CCSource>();
    Camera m_Camera;

    Queue<GameObject> m_IndicatorQueue = new Queue<GameObject>();
    Dictionary<CCSource, GameObject> m_IndicatorMap = new Dictionary<CCSource, GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        if (s_Instance != null)
        {
            Destroy(this);
            return;
        }
        
        s_Instance = this;
    }

    void Start()
    {
        m_Camera = Camera.main;
        Database.BuildMap();

        const int indicatorPool = 8;
        for(int i = 0; i < indicatorPool; ++i)
        {
            var indicator = Instantiate(IndicatorPrefab, IndicatorCanvas.transform);
            indicator.transform.localPosition = Vector3.zero;
            indicator.transform.localRotation= Quaternion.identity;
            
            indicator.SetActive(false);
            m_IndicatorQueue.Enqueue(indicator);
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < m_Sources.Count; ++i)
        {
            if(m_Sources[i].Displayed)
                m_Sources[i].Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = m_Camera.transform.position;
        Vector3 cameraForward = m_Camera.transform.forward;

        IndicatorCanvas.transform.position = cameraPosition + cameraForward * 0.5f;
        IndicatorCanvas.transform.forward = cameraForward;
        
        for (int i = 0; i < m_Sources.Count; ++i)
        {
            if (m_Sources[i].IsPlaying)
            {
                Vector3 toObject = m_Sources[i].transform.position - cameraPosition;
                float distance = toObject.magnitude;
                
                toObject.Normalize();
                float angle = Vector3.Dot(toObject, cameraForward);

                if (distance <= m_Sources[i].MaxDistance && angle > 0.6f)
                {
                    // facing camera
                    m_Sources[i].Display(toObject, Database);

                    if (m_Sources[i].AlwaysTracked)
                    {
                        GameObject indicator;
                        if (m_IndicatorMap.TryGetValue(m_Sources[i], out indicator))
                        {
                            indicator.SetActive(false);
                            m_IndicatorQueue.Enqueue(indicator);
                            m_IndicatorMap.Remove(m_Sources[i]);
                        }
                    }
                }
                else
                {
                    //not facing

                    if (m_Sources[i].Displayed)
                        m_Sources[i].Hide();

                    if (m_Sources[i].AlwaysTracked)
                    {
                        //display a center pointer toward the source
                        GameObject indicator;
                        if (!m_IndicatorMap.TryGetValue(m_Sources[i], out indicator) && m_IndicatorQueue.Count > 0)
                        {
                            var newInd = m_IndicatorQueue.Dequeue();
                            m_IndicatorMap[m_Sources[i]] = newInd;
                            indicator = newInd;
                            indicator.SetActive(true);
                        }

                        if (indicator != null)
                        {
                            Vector3 onPlane = m_Sources[i].transform.position - IndicatorCanvas.transform.position;
                            Debug.DrawLine(IndicatorCanvas.transform.position, IndicatorCanvas.transform.position + onPlane, Color.yellow);

                            float proj = Vector3.Dot(onPlane, IndicatorCanvas.transform.forward);
                            onPlane -= proj * cameraForward;

                            Debug.DrawLine(IndicatorCanvas.transform.position, IndicatorCanvas.transform.position + onPlane.normalized * 2.0f, Color.magenta);

                            float planeAngle = Vector3.SignedAngle(IndicatorCanvas.transform.up, onPlane.normalized, cameraForward);
                            indicator.transform.localRotation = Quaternion.Euler(0, 0, planeAngle);
                        }
                    }
                }
            }
            else
            { //source isn't playing if it got a marker, we remove it.
                
                if(m_Sources[i].Displayed)
                    m_Sources[i].Hide();
                
                GameObject indicator;
                if (m_IndicatorMap.TryGetValue(m_Sources[i], out indicator))
                {
                    indicator.SetActive(false);
                    m_IndicatorQueue.Enqueue(indicator);
                    m_IndicatorMap.Remove(m_Sources[i]);
                }
            }
        }
    }

    public static void RegisterSource(CCSource source)
    {
        s_Instance.m_Sources.Add(source);
    }

    public static void RemoveSource(CCSource source)
    {
        s_Instance.m_Sources.Remove(source);
    }
}
