using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]

public class vrselfControl : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    public Transform leftControler = null;
    public Transform rightControler = null;
    public CameraSelector cameraSelector;
    public float groundLevel = 0.1f;
    private Transform lookObj = null;

    private Vector3 RightFootWork;
    private Vector3 LeftFootWork;
    private float FootDistance = 0.3f;        //since we don't close our feet usually, there is an FootDistance between them
    private Vector3 rightHandPosition;
    private Vector3 leftHandPosition;
    private Quaternion rightHandRotation;
    private Quaternion leftHandRotation;
    private double bodyRotation;
    public float headHeight = 0.42f;        //since vrself does not have a head but a camera, the camera needs to hover over the neck by (headHeight)
    public float headWidth = 0.1f;         //just hovering over the neck would lete you have insight on your own throat, the headWidth controls distance of your eyes to your body center

    private int getPositionInt = 0;
    [SerializeField]
    private int getPositionEveryNFrames = 10;
    private List<Vector2> positionTracking = new List<Vector2>();

    void Start()
    {
        lookObj = AllStudentAttributes.Teacher;
        animator = GetComponent<Animator>();        //get vrself
        //float headHeight = camera.transform.localPosition.y;
        //float scale = defaultHeight / headHeight;
        //transform.localScale = Vector3.one * scale;
    }

    private void Update()
    {
        lookObj = cameraSelector.activeCameraObject.transform;
        getPositionInt++;
        if (lookObj != null)
        {
            bodyRotation = Math.PI / 180 * lookObj.eulerAngles.y;

            // compute position.
            transform.position = new Vector3(lookObj.position.x - headWidth * Convert.ToSingle(Math.Sin(bodyRotation)), lookObj.position.y - headHeight, lookObj.position.z - headWidth * Convert.ToSingle(Math.Cos(bodyRotation)));
            // compute rotation.
            transform.rotation = Quaternion.Euler(0, lookObj.eulerAngles.y, 0);
        }
        if (getPositionInt == getPositionEveryNFrames)
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);
            positionTracking.Add(currentPosition);
            getPositionInt = 0;
        }
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                if (lookObj != null)
                {
                    RightFootWork = new Vector3(transform.position.x + FootDistance * Convert.ToSingle(Math.Cos(bodyRotation))        //calculate position of right foot with footdistance
                        , groundLevel
                        , transform.position.z - FootDistance * Convert.ToSingle(Math.Sin(bodyRotation)));
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0.5f);                                                     //your feet try to reach the ground with IK
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, RightFootWork);

                    LeftFootWork = new Vector3(transform.position.x - FootDistance * Convert.ToSingle(Math.Cos(bodyRotation))        //calculate position of right foot with footdistance
                        , groundLevel
                        , transform.position.z + FootDistance * Convert.ToSingle(Math.Sin(bodyRotation)));
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0.5f);                                                      //your feet try to reach the ground with IK
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootWork);
                }
                // Set the hand positions, if they were assigned beforehand
                if ((rightControler != null) && (leftControler != null))
                {
                    rightHandPosition = new Vector3(rightControler.position.x, rightControler.position.y, rightControler.position.z);       //here the handOffset needs to find a place. 
                    leftHandPosition = new Vector3(leftControler.position.x, leftControler.position.y, leftControler.position.z);          //here the handOffset needs to find a place. 
                    rightHandRotation = new Quaternion(rightControler.rotation.x, rightControler.rotation.y, rightControler.rotation.z, rightControler.rotation.w);    //do NOT change the values like this
                    leftHandRotation = new Quaternion(leftControler.rotation.x, leftControler.rotation.y, leftControler.rotation.z, leftControler.rotation.w);       //do NOT change the values like this
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);           //set all the values that were calculated for hands.
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandRotation);
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRotation);
                }

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

    private void OnDisable()
    {
        MenuDataHolder.evaluationMap = positionTracking;
        MenuMethods.Save();
    }

}