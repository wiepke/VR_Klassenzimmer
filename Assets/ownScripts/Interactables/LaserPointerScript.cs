using UnityEngine;

// a SteamVR solution for Laserpointers
public class LaserPointerScript : MonoBehaviour
{
    /*
    public SteamVR_LaserPointer laserPointer;

    public void Awake()
    {
        if (laserPointer != null)
        {
            laserPointer.PointerIn += PointerInside;
            laserPointer.PointerOut += PointerOutside;
            laserPointer.PointerClick += PointerClick;
        }
        else
        {
            Debug.LogWarning("You forgot to initialize the laserPointer");
        }
        
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.CompareTag("StudentSlot"))
        {
            BehaviourController bc = e.target.gameObject.GetComponent<BehaviourController>();
            if (bc.CurrentBehaviour == "RaiseArm")
            {
                bc.HandleBehaviour("AskQuestion");
            }
            else
            {
                bc.TriggerLastGoodBehaviour();
            }
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.CompareTag("StudentSlot"))
        {
            laserPointer.color = Color.red;
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.CompareTag("StudentSlot"))
        {
            laserPointer.color = Color.black;
        }
    }*/
}