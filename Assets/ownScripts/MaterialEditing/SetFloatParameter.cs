using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Sets a float value within the domain [0,1] for the given material property.
/// </summary>
[ExecuteInEditMode]
public class SetFloatParameter : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float Value = 0f;

    public string Parameter = "Tile";

    private MaterialPropertyBlock mpb;
    private Renderer r;

    private void Start()
    {
        Init();
        UpdateValue();
    }

    public void Init()
    {
        mpb = new MaterialPropertyBlock();
        r = GetComponent<Renderer>();
    }

    public void UpdateValue()
    {
        r.GetPropertyBlock(mpb);
        mpb.SetFloat(Parameter, Value);
        r.SetPropertyBlock(mpb);
    }

#if UNITY_EDITOR
    private void Update()
    {
        Init();
        UpdateValue();
    }
#endif
}
