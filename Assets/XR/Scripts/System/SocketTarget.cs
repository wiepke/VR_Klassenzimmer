using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Special script that work with the XRExclusiveSocket script. This allow to define a SocketType and if that SocketType
/// does not match the XRExclusiveSocket SocketType, this won't be accepted by the socket as a valid target
/// </summary>
[RequireComponent(typeof(XRBaseInteractable))]
public class SocketTarget : MonoBehaviour
{
    public string SocketType;
    public XRInteractableEvent SocketedEvent;
    public bool DisableSocketOnSocketed;
    
    void Awake()
    {
        var interactable = GetComponent<XRBaseInteractable>();

        interactable.onSelectEnter.AddListener(SelectedSwitch);
    }

    public void SelectedSwitch(XRBaseInteractor interactor)
    {
        var socketInteractor = interactor as XRExclusiveSocketInteractor;
        
        if(socketInteractor == null)
            return;

        if(SocketType != socketInteractor.AcceptedType)
            return;

        if (DisableSocketOnSocketed)
        {
            //TODO : find a better way, delay feel very wrong
            StartCoroutine(DisableSocketDelayed(socketInteractor));
        }

        SocketedEvent.Invoke(interactor);
    }

    IEnumerator DisableSocketDelayed(XRExclusiveSocketInteractor interactor)
    {
        yield return new WaitForSeconds(0.5f);
        interactor.socketActive = false;
    }
}
