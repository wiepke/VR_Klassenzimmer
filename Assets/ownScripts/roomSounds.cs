using System;
using UnityEngine;

public class roomSounds : MonoBehaviour {
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;

    private int noiseLevel;

    private AudioSource source;

    void Start()
    {
        noiseLevel = 0;
        source = GetComponent<AudioSource>();
        source.enabled = true;
        source.Play();
    }

    public void moreNoise(String direction)
    {
        if (direction.Equals("up"))
            if (noiseLevel < 3)
            {
                noiseLevel += 1;
            }
        switch (noiseLevel)
        {
            case 0:
                Debug.Log("leise");
                source.volume = 0.7f;
                source.clip = level1;
                break;
            case 1:
                Debug.Log("medium");
                source.volume = 0.5f;
                source.clip = level2;
                break;
            case 2:
                Debug.Log("laut");
                source.volume = 0.8f;
                source.clip = level2;
                break;
        }
    }

    public void lessNoise(String direction)
    {
        if (direction.Equals("up"))
        {
            if (noiseLevel < 3)
            {
                noiseLevel += 1;
            }
        }
        if (direction.Equals("down"))
        {
            if (noiseLevel > 0)
            {
                noiseLevel -= 1;
            }
        }
    
        switch (noiseLevel)
        {
            case 0:
                Debug.Log("leise");
                source.volume = 0.8f;
                source.clip = level1;
                source.Play();
                break;
            case 1:
                Debug.Log("medium");
                source.volume = 0.7f;
                source.clip = level2;
                source.Play();
                break;
            case 2:
                Debug.Log("laut");
                source.volume = 1f;
                source.clip = level2;
                source.Play();
                break;
        }
    }

    
}
