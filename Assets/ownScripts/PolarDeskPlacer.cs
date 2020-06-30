using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarDeskPlacer : DeskPlacer
{
    /// <summary>
    /// Radius to the innermost chair circle.
    /// </summary>
    [Range(0f, 10f)]
    public float InnerRadius;

    /// <summary>
    /// Chairs in the innermost circle.
    /// </summary>
    public int ChairsInInnerCircle = 5;

    /// <summary>
    /// Number of chairs added with each new circle.
    /// </summary>
    public int AdditionalChairs = 0;

    /// <summary>
    /// Distance betwean each chair circle.
    /// </summary>
    [Range(0f, 10f)]
    public float CircleDistance;

    public override void UpdateDeskTransform(int index, Transform desk)
    { // TODO something here is simply wrong
        int circle = 0;
        for (int i = index; i > 0; i -= ChairsInInnerCircle + circle * AdditionalChairs) circle++;

        int chairsInCircle = ChairsInInnerCircle + circle * AdditionalChairs;
        float angle = 360f / ((float)chairsInCircle);

        int chair = index;
        for (int i = circle; i > 0; i--) chair -= ChairsInInnerCircle + i * AdditionalChairs;

        Quaternion rot = Quaternion.Euler(new Vector3(0f, angle * chair, 0));
        desk.localPosition = rot * new Vector3(InnerRadius + CircleDistance * circle, 0f);
        desk.localRotation = Quaternion.Euler(new Vector3(0f, angle * chair + 90f, 0));
    }

    private void OnValidate()
    {
        UpdateDesks();
    }
}
