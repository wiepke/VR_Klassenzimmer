using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = true;//if active their head follows the teacher

    [SerializeField]
    private float ikIntensity = 0.8f;           //
    private Transform lookObj = null;        //lookobj should be the teacher.

    void Start()
    {
        lookObj = GameObject.Find("Students").GetComponent<ConfigureStudent>().LookObj;

        animator = GetComponent<Animator>();
    }

    public void setIkIntensity(float intensity)
    {
        ikIntensity = intensity;
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (ikActive)
        {
            if (animator)
            {

                //set position and rotation in IKIntensity towards the lookObj. 
                // Set the look target position, if one has been assigned
                if (lookObj != null)
                {
                    //Vector3 lookObjOffset = new Vector3(0, 0.5f, 1.5f);
                    animator.SetLookAtWeight(ikIntensity);        //the parameter of this function sets the percentage of "effort" the avatar uses to look in direction of lookObj
                    animator.SetLookAtPosition(lookObj.position);// -lookObjOffset);
                }

                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetLookAtWeight(0);
                }
            }
        }
        
    }
}