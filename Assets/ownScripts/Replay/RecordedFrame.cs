using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class RecordedFrame
{
    public float timeStamp;

    // Save of VR-Selfcontrol script
    [XmlElement]
    public Saved_Transform vrSelf;
    public Saved_Transform leftControler;
    public Saved_Transform rightControler;
    public Saved_Transform leftHandRenderModel;
    public Saved_Transform rightHandRenderModel;
    public Saved_Transform lookObj;
    public Vector3 oldPosition;
    public Vector3 RightFootWork;
    public Vector3 LeftFootWork;
    public Vector3 rightHandPosition;
    public Vector3 leftHandPosition;
    public Quaternion rightHandRotation;
    public Quaternion leftHandRotation;
    public double bodyRotation;

    [XmlArray]
    public List<Saved_Behaviour> savedBehaviours = new List<Saved_Behaviour>();
    [XmlArray]
    public List<Saved_Transform> savedInteractables = new List<Saved_Transform>();


    public void SetupRecordData(vrselfControl vrselfControlScript, float timeStamp, Transform leftHandRenderModel, Transform rightHandRenderModel)
    {
        this.timeStamp = timeStamp;

        this.vrSelf = new Saved_Transform(vrselfControlScript.transform);
        this.leftControler = new Saved_Transform(vrselfControlScript.leftControler);
        this.rightControler = new Saved_Transform(vrselfControlScript.rightControler);
        this.lookObj = new Saved_Transform(vrselfControlScript.lookObj);

        if (leftHandRenderModel != null)
        {
            this.leftHandRenderModel = new Saved_Transform(leftHandRenderModel);
        }

        if (rightHandRenderModel != null)
        {
            this.rightHandRenderModel = new Saved_Transform(rightHandRenderModel);
        }

        this.oldPosition = vrselfControlScript.GetOldPosition();
        this.RightFootWork = vrselfControlScript.GetRightFootWork();
        this.LeftFootWork = vrselfControlScript.GetLeftFootWork();
        this.rightHandPosition = vrselfControlScript.GetRightHandPosition();
        this.leftHandPosition = vrselfControlScript.GetLeftHandPosition();
        this.rightHandRotation = vrselfControlScript.GetRightHandRotation();
        this.leftHandRotation = vrselfControlScript.GetLeftHandRotation();
        this.bodyRotation = vrselfControlScript.GetBodyRotation();
    }

    public void SaveObjectTransforms(List<Transform> transformsToBeSaved)
    {
        foreach (Transform currentTransform in transformsToBeSaved)
        {
            Saved_Transform transformToBeSaved = new Saved_Transform(currentTransform, currentTransform.name);
            savedInteractables.Add(transformToBeSaved);
        }
    }

    #region experimental

    /*

    public void SetupRecordData(float elapsedTime, List<Transform> transformsToBeSaved)
    {
        for (int index_transform = 0; index_transform < transformsToBeSaved.Count; index_transform++) // foreach transform in transformsToBeSaved (not using foreach because need index)
        {
            Transform currentTransform = transformsToBeSaved[index_transform];

            Saved_Transform transformToBeSaved = new Saved_Transform(currentTransform, currentTransform.name);
            savedTransforms.Add(transformToBeSaved);

        }
        timeStamp = elapsedTime;
    }*/
    #endregion

}

public class Saved_Behaviour
{
    [XmlElement]
    public string student;

    [XmlElement]
    public string performedBehaviour;

    public Saved_Behaviour()
    {

    }

    public Saved_Behaviour(string bc, string behaviour)
    {
        student = bc;
        performedBehaviour = behaviour;
    }

    public string GetBehaviourController()
    {
        return student;
    }

    public string GetPerformedBehaviour()
    {
        return performedBehaviour;
    }

    public void SetBehaviourController(string bc)
    {
        student = bc;
    }

    public void SetPerformedBehaviour(string behaviour)
    {
        performedBehaviour = behaviour;
    }


}


public class Saved_Transform
{
    [XmlAttribute]
    public string name_Gameobject;

    [XmlElement]
    public float posX;
    [XmlElement]
    public float posY;
    [XmlElement]
    public float posZ;

    [XmlElement]
    public float rotX;
    [XmlElement]
    public float rotY;
    [XmlElement]
    public float rotZ;

    public Saved_Transform()
    {

    }

    public Saved_Transform(Transform transform, string name)
    {
        name_Gameobject = name;

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;
        rotZ = transform.eulerAngles.z;
    }

    public Saved_Transform(Transform transform)
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;
        rotZ = transform.eulerAngles.z;
    }

    public void SetPosition(Vector3 position)
    {
        posX = position.x;
        posY = position.y;
        posZ = position.z;
    }

    public void SetRotation(Vector3 rotation)
    {
        rotX = rotation.x;
        rotY = rotation.y;
        rotZ = rotation.z;
    }

    public Vector3 GetReconstructedPosition()
    {
        return new Vector3(posX, posY, posZ);
    }

    public Vector3 GetReconstructedRotation()
    {
        return new Vector3(rotX, rotY, rotZ);
    }

}