
using UnityEngine;

public class cameraLookat : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    void FixedUpdate()
    {
        transform.LookAt(target);
    }
}
