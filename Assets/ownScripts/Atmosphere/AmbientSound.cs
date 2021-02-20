using System;
using UnityEngine;

public class AmbientSound : MonoBehaviour {
    private int CurrentNoiseLevel = 0;

    private float[] NoiseLevels = { 0.5f, 0.7f, 0.8f, 1.0f };
    
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.enabled = true;

        if (MenuDataHolder.isExperiment)
        {
            /*
            if (MenuDataHolder.NumberOfExperiment == 2)
                source.PlayOneShot(SoundLoader.Instance.ExperimentAmbient[2], 1f);
            else if (MenuDataHolder.isPresentation)
                source.PlayOneShot(SoundLoader.Instance.ExperimentAmbient[0], 1f);
            else
                source.PlayOneShot(SoundLoader.Instance.ExperimentAmbient[1], 1f);
            */
        }
        else
        {
            source.clip = ClassAmbient(CurrentNoiseLevel);
            source.Play();
        }   
    }

    public void SoundLevel(AmbientChange ac)
    {
        if (ac.level < 0 || ac.level > NoiseLevels.Length)
            throw new ArgumentOutOfRangeException("A audio level between 0 and " + NoiseLevels.Length + " is expected");

        string currClip = source.clip.name;
        CurrentNoiseLevel = ac.level;
        source.volume = NoiseLevels[CurrentNoiseLevel];
        source.clip = ClassAmbient(CurrentNoiseLevel);

        // Only play a clip from the start if it was actually changed (avoid obvious cutoff/looping)
        if (source.clip.name != currClip) source.Play();
    }

    public AudioClip ClassAmbient(int level)
    {
        return SoundLoader.Instance.ClassAmbient[level / 2];
    }
}
