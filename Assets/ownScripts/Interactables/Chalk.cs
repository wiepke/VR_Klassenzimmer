using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk : MonoBehaviour
{
    private bool drawEnabled = false;
    [SerializeField]
    GameObject ChalkObj = default;

    [SerializeField]
    GameObject ChalkLine = default;

    Vector3 lastLine;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ChalkObj)
        {
            GameObject trail = Instantiate(ChalkLine);
            trail.transform.position = new Vector3(ChalkObj.transform.position.x, ChalkObj.transform.position.y, -8.3734f);
            lastLine = trail.transform.position;
            drawEnabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == ChalkObj)
        {
            drawEnabled = false;
        }
    }

    void Update()
    {
        if (drawEnabled)
        {
            if (Vector3.Distance(lastLine, transform.position) > 0.2f)
            {
                GameObject trail = Instantiate(ChalkLine);
                trail.transform.parent = transform;
                trail.transform.position = new Vector3(ChalkObj.transform.position.x, ChalkObj.transform.position.y, -8.3734f);
            }
        }
    }
}
