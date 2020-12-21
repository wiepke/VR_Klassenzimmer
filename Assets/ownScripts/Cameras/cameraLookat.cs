
using UnityEngine;

public class cameraLookat : MonoBehaviour
{
    [SerializeField]
    private Transform target = default;

    void FixedUpdate()
    {
        transform.LookAt(target);
    }
}
