using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource source;
    private AudioSource phone;
    private float volumeFloat = 0.7f;

    private StudentController sc;
    // used when isExperiment == true
    private int maleCount = 0, femaleCount = 0;

    private SoundLoader Clips;

    void Start()
    {
        // Force static resource loader to load all sound clips (just once)
        Clips = SoundLoader.Instance;
        var parent = transform.parent.gameObject;
        source = parent.GetComponent<AudioSource>();
        sc = parent.GetComponent<StudentController>(); // TODO: Needed?
    }

    public void PlayClip(List<AudioClip> clips, int count, float volume)
    {
        source.enabled = true;
        source.pitch = 1f;
        int index = count % clips.Count;

        source.PlayOneShot(clips[index], volume);
    }

    public void PlayClip(List<AudioClip> clips, float volume)
    {
        PlayClip(clips, Random.Range(0, clips.Count), volume);
    }

    public void PlayClip(List<AudioClip> clips, int count)
    {
        PlayClip(clips, count, volumeFloat);
    }

    public void PlayClip(List<AudioClip> clips)
    {
        PlayClip(clips, volumeFloat);
    }

    public void AskQuestion(bool isMale)
    {
        List<AudioClip> clips = isMale ? Clips.MaleQuestions : Clips.FemaleQuestions;
        if (MenuDataHolder.isExperiment) {
            PlayClip(clips, isMale ? maleCount : femaleCount);
            if (isMale) maleCount++; else femaleCount++;
        }
        else PlayClip(clips);
    }

    public void GroupChat(bool originMale, bool receiverMale, bool disruptive)
    {
        List<AudioClip> clips;

        if (originMale != receiverMale) // fm
        {
            clips = disruptive ? Clips.MaleFemaleDistortion : Clips.MaleFemaleWork;
        }
        else if (originMale) // mm
        {
            clips = disruptive ? Clips.MaleMaleDistortion : Clips.MaleMaleWork;
        }
        else // ff
        {
            clips = disruptive ? Clips.FemaleFemaleDistortion : Clips.FemaleFemaleWork;
        }

        PlayClip(clips, volumeFloat / 2f);
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
        var originMale = sc.StudentObj.IsMale;

        if (inGroup)
        {
            var receiverMale = sc.Behaviour.ConversationPartner.gameObject
                .GetComponent<StudentController>().IsMale; // TODO: avoid
            GroupChat(originMale, receiverMale, distortion);
        }
        else
        {
            AskQuestion(originMale);
        }
    }

    private void appleSound()
    {
        source.enabled = true;
        source.pitch = 0.4f;
        source.PlayOneShot(Clips.AppleBite, volumeFloat - 0.4f);
    }

    private void biteSound()
    {
        source.enabled = true;
        source.pitch = 0.5f;
        source.PlayOneShot(Clips.JustBite, volumeFloat - 0.8f);
    }

    private void sodaCanSound()
    {
        source.enabled = true;
        source.pitch = 0.5f;
        source.PlayOneShot(Clips.OpenSodaCan, volumeFloat - 0.8f);
    }

    private void punchSound()
    {
        source.enabled = true;
        source.pitch = 1f;
        source.PlayOneShot(Clips.PunchSound, volumeFloat - 0.4f);
    }

    public void stopVoice()
    {
        if (source != null)
        {
            source.enabled = false;
        }
    }

    // TODO: Unused?
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
