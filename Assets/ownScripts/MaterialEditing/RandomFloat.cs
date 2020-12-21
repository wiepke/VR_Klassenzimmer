using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomizes a given shader parameter as a material property block once at the start of a scene.
/// </summary>
[ExecuteInEditMode]
public class RandomFloat : MonoBehaviour
{
    public string ParameterName = "Tile";

    // TODO: Support setting a seed, possibly by rolling in the editor
    private void Start()
    {
        var mpb = new MaterialPropertyBlock();
        var rend = GetComponent<Renderer>();
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat(ParameterName, Random.value);
        rend.SetPropertyBlock(mpb);
    }
}