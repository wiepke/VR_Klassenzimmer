using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractor))]
public class ActivateRotatingInteractor : MonoBehaviour
{
    public DialInteractable DialToActivate;

    XRBaseInteractor m_Interactor;
    void Start()
    {
        m_Interactor = GetComponent<XRBaseInteractor>();
        m_Interactor.onSelectEnter.AddListener(Activated);
    }

    void Activated(XRBaseInteractable interactable)
    {
        DialToActivate.RotatingRigidbody = interactable.GetComponentInChildren<Rigidbody>();
        DialToActivate.gameObject.SetActive(true);

        interactable.interactionLayerMask = 0;
    }
}
