using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    Rigidbody rigid;
    private System.Random rnd = new System.Random();

    /// <summary>
    /// Force of the throw on release.
    /// </summary>
    public float ThrowForce = 500f;

    /// <summary>
    /// Max deviation added ontop of throw direction.
    /// </summary>
    public float Deviation = 0.1f;

    public void ReleaseMe(Vector3 direction)
    {
        rigid = GetComponent<Rigidbody>();
        transform.parent = null;
        rigid.useGravity = true;
        rigid.isKinematic = false;

        direction = direction.normalized + Random.insideUnitSphere * Deviation;
        rigid.AddForce(direction * ThrowForce);
    }
}
