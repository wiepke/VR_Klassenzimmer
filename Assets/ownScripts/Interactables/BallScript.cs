using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    private Rigidbody rigid;
    private System.Random rnd = new System.Random();

    /// <summary>
    /// Force of the throw on release.
    /// </summary>
    private float throwForce = 500f;

    /// <summary>
    /// Max deviation added ontop of throw direction.
    /// </summary>
    private float deviation = 0.1f;

    public void ReleaseMe(Vector3 direction)
    {
        rigid = GetComponent<Rigidbody>();
        transform.parent = null;
        rigid.useGravity = true;
        rigid.isKinematic = false;

        direction = direction.normalized + Random.insideUnitSphere * deviation;
        rigid.AddForce(direction * throwForce);
    }
}
