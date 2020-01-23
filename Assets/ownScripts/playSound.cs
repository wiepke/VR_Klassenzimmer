using System;
using System.Collections.Generic;
using UnityEngine;

public class playSound : MonoBehaviour
{
    private AudioClip appleBite;
    private AudioClip justBite;
    private List<AudioClip> maleFemaleDistortion = new List<AudioClip>();
    private List<AudioClip> maleMaleDistortion = new List<AudioClip>();
    private List<AudioClip> femaleFemaleDistortion = new List<AudioClip>();
    private List<AudioClip> maleFemaleWork = new List<AudioClip>();
    private List<AudioClip> maleMaleWork = new List<AudioClip>();
    private List<AudioClip> femaleFemaleWork = new List<AudioClip>();
    private List<AudioClip> femaleQuestions = new List<AudioClip>();
    private List<AudioClip> maleQuestions = new List<AudioClip>();
    private float volumeFloat = 0.7f;

    private AudioClip punch;
    private AudioClip openSodaCan;
    private AudioSource source;
    private AudioSource phone;

    void Start()
    {
        maleMaleDistortion.Add(Resources.Load<AudioClip>("soundFiles/Aufnahme #11"));
        maleMaleDistortion.Add(Resources.Load<AudioClip>("soundFiles/Aufnahme #12"));
        maleMaleDistortion.Add(Resources.Load<AudioClip>("soundFiles/Aufnahme #13"));

        maleFemaleDistortion.Add(Resources.Load<AudioClip>("soundFiles/Aufnahme #13"));
        maleFemaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 1"));

        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 1"));
        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 2"));
        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 3"));
        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 4"));
        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 5"));
        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 6"));
        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 7"));
        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 8"));
        maleMaleWork.Add(Resources.Load<AudioClip>("soundFiles/Situation 9"));
        femaleFemaleDistortion.Add(Resources.Load<AudioClip>("soundFiles/Gespraech - Bauernhof-Fuchs"));
        femaleFemaleDistortion.Add(Resources.Load<AudioClip>("soundFiles/Gespraech - unnoetig"));
        femaleFemaleDistortion.Add(Resources.Load<AudioClip>("soundFiles/Gespraech - Kino-BibiBlogsBerg"));
        femaleFemaleWork.Add(Resources.Load<AudioClip>("soundFiles/Aufgabe - Aufgabenskizze 3"));
        femaleFemaleWork.Add(Resources.Load<AudioClip>("soundFiles/Aufgabe - fertig-mit-Drittens"));
        femaleFemaleWork.Add(Resources.Load<AudioClip>("soundFiles/Aufgabe - Mindmap-Aufbau"));
        femaleFemaleWork.Add(Resources.Load<AudioClip>("soundFiles/Aufgabe - roter-Filzstift"));
        femaleFemaleWork.Add(Resources.Load<AudioClip>("soundFiles/Aufgabe - Uebersicht-Fuchs"));
        femaleFemaleWork.Add(Resources.Load<AudioClip>("soundFiles/Aufgabe - Mindmap-Anfang"));
        femaleFemaleWork.Add(Resources.Load<AudioClip>("soundFiles/Aufgabe - schnell-vergleichen"));

        femaleQuestions.Add(Resources.Load<AudioClip>("soundFiles/Anton lenkt ab-female"));
        //femaleQuestions.Add(Resources.Load<AudioClip>("soundFiles/kapier ich nicht-female"));
        femaleQuestions.Add(Resources.Load<AudioClip>("soundFiles/kann ich was essen-female"));
        femaleQuestions.Add(Resources.Load<AudioClip>("soundFiles/noch mal erklaeren-female"));
        femaleQuestions.Add(Resources.Load<AudioClip>("soundFiles/wozu brauchen wir das-female"));
        femaleQuestions.Add(Resources.Load<AudioClip>("soundFiles/Hunger-female"));

        maleQuestions.Add(Resources.Load<AudioClip>("soundFiles/Pause machen-male"));
        maleQuestions.Add(Resources.Load<AudioClip>("soundFiles/frueher Schluss machen-male"));
        maleQuestions.Add(Resources.Load<AudioClip>("soundFiles/Herr Schmidt-male"));
        maleQuestions.Add(Resources.Load<AudioClip>("soundFiles/kapier ich nicht-male"));


        appleBite = Resources.Load<AudioClip>("soundFiles/AppleBite");
        justBite = Resources.Load<AudioClip>("soundFiles/Bite");
        openSodaCan = Resources.Load<AudioClip>("soundFiles/Soda-can-opening-sound-effect");
        punch = Resources.Load<AudioClip>("soundFiles/punch");
        source = GetComponent<AudioSource>();
    }

    public void disturbanceTalk()
    {
        talk(true, true);
    }

    public void questionTalk()
    {
        talk(false, false);
    }

    public void groupWorkTalk()
    {
        talk(false, true);
    }

    private void talk(bool distortion, bool inGroup)
    {
        Animator animator;
        String myGender = "male";
        for (int i = 0; i < transform.childCount; i++)
        {
            if (gameObject.name.Contains("Fem"))
            {
                myGender = "female";
                break;
            }
        }

        if (inGroup)
        {
            String place = transform.parent.name;
            String partnerSits = "";
            if (place.Contains("R"))
            {
                place = place.Replace("R", "L");
                partnerSits = "L";
            }
            else
            {
                place = place.Replace("L", "R");
                partnerSits = "R";
            }
            DateTime localDate = DateTime.Now;
            GameObject partnerStudent = GameObject.Find("classroom-scaler/Students/" + place).transform.GetChild(0).gameObject;
            StudentAttributes attributes = ConfigureStudent.studentAttributes[partnerStudent];
            attributes.isDistorting = true;
            attributes.TimeDelayToLastMisbehaviour = localDate.Minute * 60 + localDate.Second;
            attributes.ChanceToMisbehave = 0f;
            ConfigureStudent.studentAttributes[partnerStudent] = attributes;
            String partnerGender = "male";
            animator = partnerStudent.GetComponent<Animator>();
            
            foreach (Transform partner in partnerStudent.transform)
            {
                if (partner.name.Contains("M3DFemale"))
                {
                    partnerGender = "female";
                    break;
                }
            }
            animator.SetTrigger("chatting" + partnerSits + "WV");
            if (myGender.Equals("female"))
            {
                if (partnerGender.Equals("female"))
                {
                    femaleFemaleTalk(distortion);

                }
                else
                {
                    maleFemaleTalk(distortion);
                }
            }
            else
            {
                if (partnerGender.Equals("female"))
                {
                    maleFemaleTalk(distortion);
                }
                else
                {
                    maleMaleTalk(distortion);
                }
            }
        }
        else
        {
            if (myGender == "female")
            {
                femaleQuestion();
            }
            else
            {
                maleQuestion();
            }
            
        }
        
    }

    private void femaleQuestion()
    {
        source.enabled = true;
        source.pitch = 1f;
        int rndSound;
        System.Random rnd = new System.Random();
        rndSound = rnd.Next(0, femaleQuestions.Count);
        source.PlayOneShot(femaleQuestions[rndSound], volumeFloat);
    }

    private void maleQuestion()
    {
        source.enabled = true;
        source.pitch = 1f;
        int rndSound;
        System.Random rnd = new System.Random();
        rndSound = rnd.Next(0, maleQuestions.Count);
        source.PlayOneShot(maleQuestions[rndSound], volumeFloat);
    }

    private void maleMaleTalk(bool distortion)
    {
        source.enabled = true;
        source.pitch = 1f;
        int rndSound;
        System.Random rnd = new System.Random();
        if (distortion)
        {
            rndSound = rnd.Next(0, maleMaleDistortion.Count);
            source.PlayOneShot(maleMaleDistortion[rndSound], volumeFloat);
        }
        else
        {
            rndSound = rnd.Next(0, maleMaleWork.Count);
            source.PlayOneShot(maleMaleWork[rndSound], volumeFloat);
        }
    }

    private void maleFemaleTalk(bool distortion)
    {
        source.enabled = true;
        source.pitch = 1f;
        int rndSound;
        System.Random rnd = new System.Random();
        if (distortion)
        {
            rndSound = rnd.Next(0, maleFemaleDistortion.Count);
            source.PlayOneShot(maleFemaleDistortion[rndSound], volumeFloat);
        }
        else
        {
            rndSound = rnd.Next(0, maleFemaleWork.Count);
            source.PlayOneShot(maleFemaleWork[rndSound], volumeFloat);
        }
        
    }

    private void femaleFemaleTalk(bool distortion)
    {
        source.enabled = true;
        source.pitch = 1f;
        int rndSound;
        System.Random rnd = new System.Random();
        if (distortion)
        {
            rndSound = rnd.Next(0, femaleFemaleDistortion.Count);
            source.PlayOneShot(femaleFemaleDistortion[rndSound], volumeFloat);
        }
        else
        {
            rndSound = rnd.Next(0, femaleFemaleWork.Count);
            source.PlayOneShot(femaleFemaleWork[rndSound], volumeFloat);
        }
        
    }

    private void appleSound()
    {
        source.enabled = true;
        source.pitch = 0.4f;
        source.PlayOneShot(appleBite, volumeFloat-0.2f);
    }

    private void biteSound()
    {
        source.enabled = true;
        source.pitch = 0.5f;
        source.PlayOneShot(justBite, volumeFloat - 0.6f);
    }

    private void sodaCanSound()
    {
        source.enabled = true;
        source.pitch = 0.5f;
        source.PlayOneShot(openSodaCan, volumeFloat - 0.6f);
    }

    private void punchSound()
    {
        source.enabled = true;
        source.pitch = 1f;
        source.PlayOneShot(punch, volumeFloat-0.2f);
    }

    private void stopVoice()
    {
        source.enabled = false;
    }

    private void PhoneAudioPlay()   //starts the audio-file attached to the objects "audio-source"
    {                               //will just be used in animation-events
                                    //be carefull to attach the correct audiofile to the person.

        phone.enabled = true;
        phone.loop = true;
    }
    private void PhoneAudioStop()        //will be stopped in the end of the animation "chatting"
    {                               //will just be used in animation-events
        phone.enabled = false;
        phone.loop = false;
    }
}