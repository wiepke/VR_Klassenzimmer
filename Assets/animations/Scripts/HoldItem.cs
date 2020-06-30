using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : StateMachineBehaviour
{
    public GameObject Item;
    private GameObject instance;
    public bool HoldRight = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<StudentAnimationEvents>().Attach(HoldRight, Item);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<StudentAnimationEvents>().Detach();
    }
}
