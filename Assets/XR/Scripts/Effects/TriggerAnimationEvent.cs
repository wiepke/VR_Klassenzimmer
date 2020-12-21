using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractor))]
public class TriggerAnimationEvent : MonoBehaviour
{
    public string TriggerName;

    int m_TriggerID;
    
    void Start()
    {
        m_TriggerID = Animator.StringToHash(TriggerName);
        var interactor = GetComponent<XRBaseInteractor>();
        interactor.onSelectEnter.AddListener(TriggerAnim);
    }

    public void TriggerAnim(XRBaseInteractable interactable)
    {
        var animator = interactable.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.SetTrigger(TriggerName);
        }

        interactable.interactionLayerMask &= ~(1<<LayerMask.NameToLayer("Hands"));
    }
}
