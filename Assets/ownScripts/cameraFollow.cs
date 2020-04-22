using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class cameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 offset;

    private double bodyRotation;

    void FixedUpdate()
    {
        bodyRotation = Math.PI / 180 * target.eulerAngles.y;
        Vector3 desiredPosition = new Vector3(  target.position.x - offset.x * Convert.ToSingle(Math.Sin(bodyRotation)),
                                                target.position.y,
                                                target.position.z - offset.z * Convert.ToSingle(Math.Cos(bodyRotation)));
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        transform.rotation = target.rotation;
    }
}
