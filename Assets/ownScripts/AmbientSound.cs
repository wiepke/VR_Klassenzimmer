using System;
using UnityEngine;

public class AmbientSound : MonoBehaviour {
    private int CurrentNoiseLevel;

    private float[] NoiseLevels = { 0.5f, 0.7f, 0.8f, 1.0f };
    public AudioClip[] NoiseLevelAudio;
    private AudioSource source;

    void Start()
    {
        CurrentNoiseLevel = 0;
        source = GetComponent<AudioSource>();
        source.enabled = true;
        source.Play();
    }

    public void SoundLevel(AmbientChange ac)
    {
        if (ac.level < 0 || ac.level > NoiseLevels.Length)
            throw new ArgumentOutOfRangeException("A audio leven between 0 and " + NoiseLevels.Length + " is expected");

        string currClip = source.clip.name;
        CurrentNoiseLevel = ac.level;
        source.volume = NoiseLevels[CurrentNoiseLevel];
        source.clip = NoiseLevelAudio[CurrentNoiseLevel];

        // Only play a clip from the start if it was actually changed (avoid obvious cutoff/looping)
        if (source.clip.name != currClip) source.Play();
    }
}
