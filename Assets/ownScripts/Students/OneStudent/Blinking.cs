using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    // Used in eyelid animation
    private SkinnedMeshRenderer bodyMesh;
    private SkinnedMeshRenderer eyeMesh = null;

    private float minBlinkDelay = 5f;
    private float maxBlinkDelay = 15f;

    private float blink = 0f;
    private float blinkSpeed = 7f; // a single blink takes 1f / blinkSpeed seconds

    private float blinkDir = 1f;

    private void Start()
    {
        StartCoroutine(WaitForBlink());

        bodyMesh = transform.Find("Body").GetComponent<SkinnedMeshRenderer>();

        // There might be no eyelashes, so ignore
        try
        {
            eyeMesh = transform.Find("Eyelashes").GetComponent<SkinnedMeshRenderer>();
        }
        catch (System.NullReferenceException) { }
    }

    private void Update()
    {
        blink = Mathf.Clamp(blink + Time.deltaTime * blinkSpeed * blinkDir, 0f, 1f);

        float blinkAnim = Mathf.SmoothStep(0f, 1f, blink) * 100f;

        bodyMesh.SetBlendShapeWeight(0, blinkAnim);
        bodyMesh.SetBlendShapeWeight(1, blinkAnim);
        
        if (eyeMesh)
        {
            eyeMesh.SetBlendShapeWeight(0, blinkAnim);
            eyeMesh.SetBlendShapeWeight(1, blinkAnim);
        }
        

        if (blink >= 1f) blinkDir = -1;
    }

    /// <summary>
    /// Waits for some random amount of time before starting of the blinking animation.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForBlink()
    {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minBlinkDelay, maxBlinkDelay));
            blinkDir = 1f;
        }
    }
}
