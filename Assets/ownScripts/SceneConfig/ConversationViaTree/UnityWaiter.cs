using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWaiter : MonoBehaviour
{
    public IEnumerator GetAudioClip(string path, GameObject student, System.Action<bool> callback)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + path, AudioType.WAV))
        {
            yield return www.SendWebRequest();
            AudioClip soundFile = DownloadHandlerAudioClip.GetContent(www);
            AudioSource audioSource = student.GetComponent<AudioSource>();
            audioSource.enabled = true;
            audioSource.PlayOneShot(soundFile);
            yield return new WaitForSeconds(soundFile.length);
            callback(true);
        }
    }

    public IEnumerator WaitToSpeak(GameObject student, System.Action<bool> callback)
    {

        yield return new WaitForSeconds(5);
        callback(true);
    }
}
