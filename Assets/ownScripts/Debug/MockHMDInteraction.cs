using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MockHMDInteraction : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float pointerRange = 20f;
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private GameObject mousePointer = default;
    private GameObject lastObjectHit = null;
    private GameObject objectToInteract = null;
    private GameObject interactableParent;
    public GameObject fallbackCam;

    Ray ray;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float rotationY = 0;

    void Start()
    {
        interactableParent = GameObject.Find("Interactables");
        characterController = GetComponent<CharacterController>();
        ray = new Ray(fallbackCam.transform.position, fallbackCam.transform.forward);
    }

    void Update()
    {        
        // Script is just active if user does not wear HMD
        translateView();
        // Player and Camera rotation
        rotateView();
        handleMouseInteraction();
    }

    private void translateView()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void rotateView()
    {
        if (Input.GetMouseButton(1))
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationY += Input.GetAxis("Mouse X") * lookSpeed;
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }

    public void rotateView(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
    }

    //interaction to hover over interactables or grab them
    private void handleMouseInteraction()
    {
        visualizeGrabPoint();

        handleHoverInteractable();

        if (lastObjectHit != null)
        {
            objectToInteract = lastObjectHit;
        }
        handleGrabInteractable();

        if (objectToInteract!= null) handleMouseWheel();
    }

    private void visualizeGrabPoint(){
        // Does the ray intersect any objects with colliders
        if (Physics.Raycast(ray, out RaycastHit hit, pointerRange)){
            mousePointer.transform.position = hit.point;
        }
    }

    private void handleHoverInteractable()
    {
        ray.origin = fallbackCam.transform.position;
        ray.direction = fallbackCam.transform.forward;
        int layerMask = 1 << 9;
        // Does the ray intersect any objects with layermask "interactable"
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            //trigger hover from UX Input
            lastObjectHit = hit.collider.gameObject;
            lastObjectHit.GetComponent<Outline>().enabled = true;
        }
        else
        {
            // the frame after something was hovered "lastObjectHit" is != null. so disable outline here
            if (lastObjectHit != null)
            {
                try
                {
                    lastObjectHit.GetComponent<Outline>().enabled = false;
                    lastObjectHit = null;
                }
                catch (Exception)
                {
                    Debug.Log(lastObjectHit.name);
                }
                
            }
        }
    }

    private void handleGrabInteractable()
    {
        if (objectToInteract != null)
        {
            HandButton handButton = objectToInteract.GetComponent<HandButton>();
            if (Input.GetMouseButton(0))
            {
                if (handButton == null)
                {
                    attachOnMouse(objectToInteract);
                }
                else
                {
                    handButton.loadScene(handButton.FunctionName);
                }
                
            }
            else
            {
                if (handButton == null)
                {
                    detachFromMouse(objectToInteract);
                    objectToInteract = null;
                }
            }
        }
    }

    //holds object to interact on on cursor (mid of screen)
    private void attachOnMouse(GameObject interactable)
    {
        interactable.transform.parent = gameObject.transform;
        interactable.GetComponent<Rigidbody>().useGravity = false;
    }

    //releases the object to Interact
    private void detachFromMouse(GameObject interactable)
    {
        interactable.GetComponent<Rigidbody>().useGravity = true;
        interactable.transform.parent = interactableParent.transform;
    } 

    //brings object to Interact closer to the camera when scrolling
    private void handleMouseWheel(){
        Vector3 directionToMove = objectToInteract.transform.localPosition * scrollSpeed;
        objectToInteract.transform.localPosition += directionToMove * Input.mouseScrollDelta.y;
    }
}
