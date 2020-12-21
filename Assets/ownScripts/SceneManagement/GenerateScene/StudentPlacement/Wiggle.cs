using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add slight amount of chaotic wiggle (i.e. rotation around origin, movement along x/y axis)
/// to any object this Behaviour is attached to.
/// </summary>
public class Wiggle : MonoBehaviour
{
    public float WiggleAngle;
    public float WiggleRange;

    void Start()
    {
        // Offset vector somewhere in a circle of range WiggleRange
        Vector3 offset = Random.onUnitSphere * WiggleRange;
        offset.y = 0;

        transform.localPosition += offset;

        // Value in range [-WiggleAngle, WiggleAngle]
        float angle = (Random.value * 2f - 1f) * WiggleAngle;

        Vector3 rotation = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(rotation + new Vector3(0f, angle, 0f));
    }
}
