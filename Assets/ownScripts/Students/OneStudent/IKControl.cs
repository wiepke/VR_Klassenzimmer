using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour
{
    public static float ViewAngle = 60f; // 120° field of vision 
    public bool IkActive = true; 
    public bool Follow { get; set; }
    public bool MoveHand { get; set; } = true;
    public float IkIntensity { get; private set; } = 0.8f; // TODO: Keep initial value and only adjust weights?
    public float IntensityDelay = 0.5f; // speed in which intensity builds up or decays
    private float intensity = 0f;
    private Transform ViewTarget { get { return AllStudentAttributes.Teacher; } }
    protected Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        bool inView = false;
        
        if (IkActive && ViewTarget)
        {
            inView = Vector3.Angle(transform.forward, ViewTarget.position - transform.position) < ViewAngle;
            animator.SetLookAtPosition(ViewTarget.position);
        }

        // Direction of decay / build-up
        intensity += (inView ? 1 : -1) * (1 / IntensityDelay) * Time.deltaTime;
        intensity = Mathf.Clamp01(intensity);

        // Smooth step for more natural progression
        float smoothedIntensity = Mathf.SmoothStep(0f, IkIntensity, intensity);
        
        animator.SetLookAtWeight(smoothedIntensity);
    }

    public void TurnTo(Transform target) 
    {
        var turnAround = Vector3.Dot(transform.forward, (target.position - transform.position).normalized) > 0;

        if (turnAround) {
            animator.SetFloat("Horizontal", Vector3.Dot(
                transform.right, (target.position - transform.position).normalized)
            );
        }

        animator.SetBool("turnAround", turnAround);
        Follow = true;
        intensity = 1.0f;
    }
}