using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableLayerChange : MonoBehaviour
{
    public XRBaseInteractable TargetInteractable;
    public LayerMask NewLayerMask;

    public void ChangeLayerDynamic(XRBaseInteractable interactable)
    {
        interactable.interactionLayerMask = NewLayerMask;
    }

    public void ChangeLayer()
    {
        TargetInteractable.interactionLayerMask = NewLayerMask;
    }
}
