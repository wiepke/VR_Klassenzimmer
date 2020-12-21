using System.Collections;
using UnityEngine;

public class PosterScript : MonoBehaviour
{
    [SerializeField]
    GameObject BoardCollider = default;

    [SerializeField]
    GameObject PosterContent = default;

    private Rigidbody rb;

    private Vector3 rota = new Vector3(90f, 0f, 0f);

    public void Awake()
    {
        StartCoroutine(PosterFade());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == BoardCollider)
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.localEulerAngles = rota;
            transform.position = new Vector3(transform.position.x, transform.position.y, -8.35f);
        }
    }
    
    public void OnTriggerExit (Collider other)
    {
        if (other.gameObject == BoardCollider)
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    public IEnumerator PosterFade()
    {
        for (float fadeAmount = 1f; fadeAmount > 0; fadeAmount -= 0.03f)
        {
            Color color = PosterContent.GetComponent<Renderer>().material.color;
            color.a = fadeAmount;
            PosterContent.GetComponent<Renderer>().material.color = color;
            yield return new WaitForSeconds(0.2f);
        }
    }
}

