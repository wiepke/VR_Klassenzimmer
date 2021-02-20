using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BehaviourController : MonoBehaviour
{
    private const float CrossFadeTime = 0.25f;

    public string CurrentBehaviour = "Idle";
    public string LastGoodBehaviour = "Idle";
    public string LastDistortion;
    public float TimeDelayToLastMisbehaviour = 0f;
    private float ChanceToMisbehave = 0f;

    public Transform ConversationPartner;

    [HideInInspector] MixamoAttachment attachment;
    [HideInInspector] PlaySound playSoundScript;

    private StudentController sc;
    private Animator animator;
    private IKControl ik;

    public bool IsDistorting { get; private set; }

    public void Init(StudentController sc)
    {
        this.sc = sc;

        playSoundScript = sc.Model.GetComponent<PlaySound>();
        attachment = sc.Model.GetComponent<MixamoAttachment>();

        animator = sc.Model.GetComponent<Animator>();
        animator.SetBool("isTall", sc.StudentObj.IsTall);
        animator.SetBool("isLeftHanded", sc.StudentObj.IsLeftHanded);
        animator.SetFloat("CycleOffset", Random.value);

        ik = sc.Model.GetComponent<IKControl>();

        if (MenuDataHolder.isNonScripted)
            StartCoroutine(NonScriptedClassBehaviour());
    }

    void OnTriggerEnter(Collider other)
    {
        if (MenuDataHolder.isAutomaticIntervention &&
            other.gameObject.transform == AllStudentAttributes.Teacher &&
            CurrentBehaviour != LastGoodBehaviour && IsDistorting)
        {
            TriggerLastGoodBehaviour();
        }
    }

    // Moves animator into specific state without using a trigger (apparently does not trigger StateMachineBehaviour.OnExitState)
    private void Transition(string state, int Layer = 0)
    {
        playSoundScript.stopVoice();
        attachment.detach();
        animator.CrossFadeInFixedTime(state, CrossFadeTime, Layer);
    }

    /// <summary>
    /// Given a partner for conversation, sets the respective attribute and calculates direction.
    /// </summary>
    /// <param name="partner">Conversation partner student object</param>
    public void FacePartner()
    {
        // Calculates a projection of a partners position onto the students local XZ-plane (i.e. (Left, Front) corresponds to partners position on unit circle)
        // This could be used later to blend cleanly between conversation angles, but should also be a fairly efficient check (i.e. Left > 0, Front > 0) in the animator
        animator.SetFloat("Horizontal", Vector3.Dot(transform.right, (ConversationPartner.position - transform.position).normalized));
        // sc.StudentAnimator.SetFloat("Front", Vector3.Dot(transform.forward, (ConversationPartner.position - transform.position).normalized));
    }

    public void TriggerLastGoodBehaviour()
    {
        MenuDataHolder.MisbehaviourSolved++;
        IsDistorting = false;
        TimeDelayToLastMisbehaviour = 0f; // DateTime.Now.Minute * 60 + DateTime.Now.Second; // TODO Where/How is this actually used?
        Behave();
    }

    private IEnumerator NonScriptedClassBehaviour()
    {
        int levelOfDisturbance = 0;

        while (true)
        {
            if (ChanceToMisbehave < Random.value)
            {
                ClassController.SetRandomDisturbance(gameObject, levelOfDisturbance);
                levelOfDisturbance++;
            }
            else
            {
                ChanceToMisbehave += 0.004f;
            }

            yield return new WaitForSeconds(10);
        }
    }
    
    public void Behave()
    {
        DisruptClass(LastGoodBehaviour, new Behave(sc.Id, LastGoodBehaviour));
    }

    public static void Disrupt(GameObject student, string disruption)
    {
        student.GetComponent<BehaviourController>().Disrupt(disruption);
    }

    // TODO: Change the method head, why explicitly state response?
    // TODO: Change name into more expressive one    
    public void DisruptClass(string disruption, JsonData response)
    {
        Disrupt(disruption);
        ClassController.Handler.Respond(disruption, response);
    }

    /// <summary>
    /// Lets student play out a given disruption.
    /// </summary>
    /// <param name="disruption">identifier of the respective disturbance</param>
    /// <param name="origin">Set to false if a student should not echo it's partner actions</param>
    public void Disrupt(string disruption, bool origin = true)
    {
        // TODO: A lot of this is probably way better to do via animator states
        Behaviour behaviour = SpecialBehaviours.GetBehaviour(disruption);

        // Set lowerBody layer weight
        animator.SetLayerWeight(1, behaviour.SupportLegs ? 1f : 0f);
        
        ik.IkActive = behaviour.IK;
        ik.Follow = !IsDistorting && LastGoodBehaviour == "Impulse";
        ik.MoveHand = behaviour.Id == "Idle";

        // Handle freshly triggered pair action
        if (behaviour.Pair && disruption != CurrentBehaviour)
        {
            // Set up facing (e.g. for blend tree)
            FacePartner();

            // Set bool to trigger animations for actors/receivers accordingly
            animator.SetBool("isReceiver", !origin);

            if (origin)
            {
                // Also disrupt neighbour/partner
                ConversationPartner.GetComponent<BehaviourController>().Disrupt(disruption, false);
            }
            else
            {
                // Notify Frontend of change
                NetworkController.Handler.Respond("behave", new Behave(sc.Id, disruption));
            }

            if (disruption == "PlakatPartner")
            {
                foreach (GameObject poster in ConfigLoader.posters) poster.SetActive(true);
            }
        }

        if (behaviour.Good)
        {
            LastGoodBehaviour = disruption;
            IsDistorting = false;
        }
        else
        {
            LastDistortion = disruption;
            IsDistorting = true;
            TimeDelayToLastMisbehaviour = 0f;
            ChanceToMisbehave = 0f;
            MenuDataHolder.MisbehaviourCount++;
        }
        CurrentBehaviour = disruption;
        Transition(disruption, 0);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BehaviourController))]
public class BehaviourControllerDebug : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying) return;

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Trigger Behaviour:");
        if (EditorGUILayout.DropdownButton(new GUIContent("Select Behaviour ..."), FocusType.Passive))
        {
            var bc = (BehaviourController)target;
            var menu = new GenericMenu();

            foreach (var state in SpecialBehaviours.BehaviourIDs)
            {
                menu.AddItem(new GUIContent(state), false, () => bc.Disrupt(state));
            }

            menu.ShowAsContext();
        }
    }
}
#endif
