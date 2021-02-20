using UnityEngine;
using TMPro;
using System;

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
        Model.AddComponent<PlaySound>();
        Model.AddComponent<Thrower>();
        Model.AddComponent<Blinking>();
        
        var ik = Model.AddComponent<IKControl>();

        Behaviour = GetComponent<BehaviourController>();
        Behaviour.Init(this);

        GetComponent<SphereCollider>().enabled = true;

        // TODO: Use algorithm instead
        string ConversationPartnerPlace = name.Contains("L") ? name.Replace("L", "R") : name.Replace("R", "L");
        Behaviour.ConversationPartner = GameObject.Find(ConversationPartnerPlace).transform;

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
