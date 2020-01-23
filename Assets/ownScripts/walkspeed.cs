using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkspeed : MonoBehaviour
{

    Vector3 lastPos = Vector3.zero;

    void Update()       //this script accelerates the Avatar in the world, since the world is larger than our real world
    {
        //The headset initializes at Vector3.zero, and remains there during Start(), so initialize lastPos here
        if (lastPos == Vector3.zero) lastPos = transform.position;
        var offset = transform.position - lastPos;
        offset.y = 0;       //position of avatar without hight.
        transform.parent.position += offset * 8f;       //8f is the extra speed in the world. Enlarge this number to become faster.
        lastPos = transform.position;
    }
}
