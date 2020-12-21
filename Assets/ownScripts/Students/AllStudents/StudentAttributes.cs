using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentAttributes
{
    public float WhenToBlink;       //stores next time for blinking with Time.Now + [5..15]seconds
    public float eyeLidPosition;    //ranges between 0 and 100. sets position of eyelids while blinking.
    public float eyeLidOpening = 5; //speed of how fast model is blinking. recommended speed between 0 and 30
    public SkinnedMeshRenderer bodyMesh;
    public SkinnedMeshRenderer eyeMesh = null;
    public Animator animator;
    public BehaviourController bc;

    public bool sitsLeft;
    public bool isMale = true;
    public bool isTall = false;
    public string name;
    public string id;

    public string CurrentBehaviour;
    public string LastGoodBehaviour;
    public string LastDistortion;
    public bool isDistorting;
    public float TimeDelayToLastMisbehaviour;
    public float ChanceToMisbehave;  //ranges between 0 and 1
}
