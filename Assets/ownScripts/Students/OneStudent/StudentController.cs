using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Generates and manages student instance logic.
/// </summary>
public class StudentController : MonoBehaviour
{
    public Student StudentObj;
    public string Id = "";

    // Properties for pass-through from Scriptable Object
    public string Name { get { return this.StudentObj.Name; } }
    public bool IsMale { get { return this.StudentObj.IsMale; } }

    // Maybe remove later?
    public GameObject Model;
    public BehaviourController Behaviour { get; private set; }

    private void Start()
    {
        if (!Model) Model = transform.GetChild(0).gameObject;

        // TODO: Just add to prefabs/objects? Won't do this until merging scenes is less of a pain
        Model.AddComponent<MixamoAttachment>();
        Model.AddComponent<playSound>();
        Model.AddComponent<thrower>();
        Model.AddComponent<Blinking>();
        
        var ik = Model.AddComponent<IKControl>();

        Behaviour = GetComponent<BehaviourController>();
        Behaviour.Init(this);

        // Maybe handle in separate components as child objects?
        if (MenuDataHolder.isAutomaticIntervention)
            GetComponent<SphereCollider>().enabled = true;
        else
            GetComponent<BoxCollider>().enabled = true;

        Behaviour.ConversationPartner = FindConversationPartner();

        try
        {
            Transform nameTag = Model.transform.Find("assecoire").Find("NameTag");
            nameTag.gameObject.SetActive(true);
            
            TextMeshProUGUI nameShield = nameTag.transform.Find("Shield").Find("Name")
                .GetComponent<TextMeshProUGUI>();
            
            nameShield.text = Name;
        }
        catch (Exception) // TODO find a more descriptive Exception
        {
            // supress warning as generated rooms don't use childed name tag
            // Debug.LogWarning("Searching for nameShield failed for Student: " + Name);
        }
    }

    private Transform FindConversationPartner()
    {
        Transform closestStudent = null;
        float closestDistanceSqr = Mathf.Infinity;
        List<GameObject> possiblePartners = new List<GameObject>(AllStudentAttributes.allStudentSlots);
        possiblePartners.Remove(gameObject);
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialPartner in possiblePartners)
        {
            Vector3 directionToPartner = potentialPartner.transform.position - currentPosition;
            float dSqrToTarget = directionToPartner.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestStudent = potentialPartner.transform;
            }
        }
        return closestStudent;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(StudentController))]
public class StudentInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
       
        StudentController student = (StudentController)target;
    }
}
#endif
