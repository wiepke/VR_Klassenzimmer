using UnityEngine;
namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class LightSwitch : MonoBehaviour
    {
        private bool isLightOn = false;
        private GameObject lever;
        private GameObject[] lights;
        private Light sun;

        //-------------------------------------------------
        void Awake()
        {
            lever = transform.Find("lightlever").gameObject;
            lights = GameObject.FindGameObjectsWithTag("lightbulb");
            sun = GameObject.Find("Sun").GetComponent<Light>();
        }


        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand)
        {
            ToggleLight();
        }

        private void ToggleLight()
        {
            isLightOn = !isLightOn;
            lever.transform.Rotate(0,0,90);
            if (!isLightOn)
            {
                foreach (var light in lights)
                {
                    var mat = light.GetComponent<Renderer>().material;
                    mat.SetColor("_EmissionColor", Color.white);
                    sun.intensity = 1.0f;
                }
            }
            else
            {
                foreach (var light in lights)
                {
                    var mat = light.GetComponent<Renderer>().material;
                    mat.SetColor("_EmissionColor", Color.black);
                    sun.intensity = 0.5f;
                }
            }
        }
    }
}