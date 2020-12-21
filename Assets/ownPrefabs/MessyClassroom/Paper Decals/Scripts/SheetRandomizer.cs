using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetRandomizer : MonoBehaviour
{
    public float AngleRange = 5f;
    public float BaseAngle = 180f;
    public float MinDestruction = 0f;
    public float MaxDestruction = 1f;

    private void Start()
    {
        Renderer r = GetComponent<Renderer>();
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();

        r.GetPropertyBlock(mpb);
        mpb.SetFloat("Destruction", Random.Range(MinDestruction, MaxDestruction));
        mpb.SetFloat("Selection", Random.value);
        mpb.SetFloat("Angle", BaseAngle + Random.Range(-AngleRange, AngleRange));
        r.SetPropertyBlock(mpb);
    }
}
