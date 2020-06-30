using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayDeskPlacer : DeskPlacer
{
    /// <summary>
    /// Number of desks in each column of desks.
    /// </summary>
    public int ColSize = 5;

    /// <summary>
    /// Distance between two columns of desks.
    /// </summary>
    [Range(0f, 10f)]
    public float ColDistance;

    /// <summary>
    /// Distance between two desks of the same column.
    /// </summary>
    [Range(0f, 10f)]
    public float DeskDistance;

    public override void UpdateDeskTransform(int index, Transform desk)
    {
        desk.localPosition = new Vector3(ColDistance * ((int)index / ColSize), 0, DeskDistance * (index % ColSize));
    }

    private void OnValidate()
    {
        UpdateDesks();
    }
}
