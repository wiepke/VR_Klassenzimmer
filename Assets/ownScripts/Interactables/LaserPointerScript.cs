using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR;
public class LaserPointerScript : MonoBehaviour
{
    [SerializeField]
    private SteamVR_LaserPointer laserPointer;

    [SerializeField]
    private GameObject laserPointerGameObject;

    public void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.CompareTag("LaserCollider"))
        {
            var bc = e.target.gameObject.GetComponentInParent<BehaviourController>();
            if (bc.CurrentBehaviour.Equals("RaiseArm"))
            {
                bc.Disrupt("AskQuestion");
                laserPointerGameObject.SetActive(false);
            }
            else if (bc.IsDistorting)
            {
                MenuDataHolder.MisbehaviourSeen++;
                laserPointerGameObject.SetActive(false);
            }
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.CompareTag("LaserCollider"))
        {
            laserPointer.color = Color.red;
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.CompareTag("LaserCollider"))
        {
            laserPointer.color = Color.black;
        }
    }
}