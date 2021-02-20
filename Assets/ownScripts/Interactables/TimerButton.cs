using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Valve.VR.InteractionSystem
{
    public class TimerButton : MonoBehaviour
    {
        private bool timerRunning = false;
       
        [SerializeField]
        private TextMeshProUGUI timerText;

        private float currentTime;
        private string minutes;
        private string seconds;

        private AudioSource source;
        private AudioClip beepSound;

        private Vector3 trans;

        void Awake()
        {
            trans = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            source = gameObject.GetComponent<AudioSource>();
            beepSound = Resources.Load<AudioClip>("soundFiles/timer_beep");
            currentTime = MenuDataHolder.TimerSeconds;
            minutes = LeadingZero((int) currentTime / 60);
            seconds = LeadingZero((int) currentTime % 60);
            timerText.text = minutes + ":" + seconds;
        }

        private void OnHandHoverBegin(Hand hand)
        {
            gameObject.transform.localPosition = new Vector3(trans.x, trans.y, trans.z - 0.1f);

            if (timerRunning)
            {
                ResetTimer();
            }
            else StartTimer();
        }

        private void OnHandHoverEnd(Hand hand)
        {
            gameObject.transform.localPosition = new Vector3(trans.x, trans.y, trans.z + 0.1f);
        }

        public void StartTimer()
        {
            timerRunning = true;
        }

        public void ResetTimer()
        {
            timerRunning = false;
            currentTime = MenuDataHolder.TimerSeconds;
            minutes = LeadingZero((int)currentTime / 60);
            seconds = LeadingZero((int)currentTime % 60);
            timerText.text = minutes + ":" + seconds;
        }

        void Update()
        {
            if (timerRunning && currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                minutes = LeadingZero((int)currentTime / 60);
                seconds = LeadingZero((int)currentTime % 60);
                timerText.text = minutes + ":" + seconds;

                if (currentTime <= 0)
                {
                    //Time is up
                    source.PlayOneShot(beepSound);
                }
            }
        }

        private string LeadingZero(int x)
        {
            return x.ToString().PadLeft(2, '0');
        }
    }
}