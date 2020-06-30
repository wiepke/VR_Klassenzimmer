using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    private EventQueue<string, string, string> queue;

    private Transform conversationPartner;
    private StudentController sc;
    private string expectedBehaviour;

    public static SocketEventHandler Handler;

    private void Start()
    {
        sc = GetComponent<StudentController>();

        queue = new EventQueue<string, string, string>(s => s, s => s);

        queue.RegisterCallback("idle", GoodBehaviour(0));
        queue.RegisterCallback("chatting", Transition("Chatting", "Init Chatting"));
        queue.RegisterCallback("hitting", Transition("Hitting", "Hitting"));
        queue.RegisterCallback("eating", Transition("eatApple"));
        queue.RegisterCallback("drinking", Transition("drinking"));
        queue.RegisterCallback("writing", GoodBehaviour(1));
        queue.RegisterCallback("question", GoodBehaviour(2));
        queue.RegisterCallback("raiseArm", GoodBehaviour(3));
        queue.RegisterCallback("behave", Transition("Good Behaviour"));
        queue.RegisterCallback("throwing", Transition("throwPaperBall"));
        queue.RegisterCallback("playing", Transition("play with accessoires"));
        queue.RegisterCallback("lethargic", Transition("letargic_starring"));
    }

    private EventQueue<string, string, string>.Handler Transition(string State)
    {
        return Transition("Base Layer", State);
    }

    private EventQueue<string, string, string>.Handler Transition(string Layer, string State)
    {
        return _ =>
        {
            Debug.Log("Should trigger " + State);
            sc.StudentAnimator.CrossFade(Layer + "." + State, 0.05f);
        };
    }

    private EventQueue<string, string, string>.Handler GoodBehaviour(int id)
    {
        return s =>
        {
            sc.StudentAnimator.SetInteger("GoodBehaviour", id);
            sc.StudentAnimator.SetTrigger("Idle");
            expectedBehaviour = s;
        };
    }

    /// <summary>
    /// Given a partner for conversation, sets the respective attribute and calculates direction.
    /// </summary>
    /// <param name="partner">Conversation partner student object</param>
    public void SetConversationPartner(Transform partner)
    {
        Debug.Log(partner);
        conversationPartner = partner;
        sc.StudentAnimator.SetBool("Front", Vector3.Dot(transform.forward, partner.position - transform.position) > 0);
        sc.StudentAnimator.SetBool("Left", Vector3.Dot(transform.right, partner.position - transform.position) > 0);
    }

    /// <summary>
    /// Let's student play out a given disruption.
    /// </summary>
    /// <param name="disruption">identifier of the respective disturbance</param>
    public void DisruptClass(string disruption)
    {
        queue.Enqueue(disruption);
        queue.ProcessAll();
    }

    public void DisruptClass(string disruption, JsonData response)
    {
        DisruptClass(disruption);
        Handler.Respond(disruption, response);
    }

    void OnTriggerEnter(Collider other)
    {
        if (MenuDataHolder.isAutomaticIntervention) // TODO check if currently well behaved somehow
        {
            MenuDataHolder.MisbehaviourSolved++;
            DisruptClass("behave", new Behave(sc.Id, expectedBehaviour));
        }
    }
}
