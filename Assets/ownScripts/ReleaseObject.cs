using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseObject : MonoBehaviour {
    public void ReleaseMe() // TODO move all of this into ThrowObject ?
    {
        transform.parent = null;
        transform.localRotation = Random.rotationUniform;

        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.AddForce(transform.forward * 500);
        rigid.useGravity = true;
    }
}
