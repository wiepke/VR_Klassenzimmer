using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPlane : MonoBehaviour
{ float speed = 0.4f;

Vector3[] verts = { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 1) };
int[] triangles = {
        0,1,2,
        1,3,2
    };

private void Start()
{
    Mesh mesh = new Mesh();
    mesh.Clear();
    mesh.vertices = verts;
    mesh.triangles = triangles;
    mesh.RecalculateNormals();
}

private void Update()
{
    float leftRight = Input.GetAxis("Horizontal");
    float upDown = Input.GetAxis("Vertical");
    this.transform.position = new Vector3(leftRight * speed, upDown * speed, 0);
}
}