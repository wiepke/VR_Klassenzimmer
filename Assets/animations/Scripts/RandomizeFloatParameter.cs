using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeFloatParameter : StateMachineBehaviour
{
    public string Parameter;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        animator.SetFloat(Parameter, Random.value);
    }
}
