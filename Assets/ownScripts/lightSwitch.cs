using UnityEngine;
namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class lightSwitch : MonoBehaviour
    {
        private bool isLightOn = false;
        private GameObject lever;
        private Light lightBack;

        //-------------------------------------------------
        void Awake()
        {
            lever = transform.Find("lightlever").gameObject;
            lightBack = GameObject.Find("classroom-scaler/classroom/roof/LampBack").GetComponent<Light>();
        }


        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand)
        {
            toggleLight();
        }

        private void toggleLight()
        {
            isLightOn = !isLightOn;
            lever.transform.Rotate(0,0,90);
            if (!isLightOn)
            {
                lightBack.intensity = 2;
            }
            else
            {
                lightBack.intensity = 0.1f;
            }
        }
    }
}