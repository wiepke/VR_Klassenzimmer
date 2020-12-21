using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWaiter : MonoBehaviour
{
    public IEnumerator GetAudioClip(string path, GameObject audioSourceObject, System.Action<bool?> callback)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            AudioClip soundFile = DownloadHandlerAudioClip.GetContent(www);
            AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
            audioSource.enabled = true;
            audioSource.pitch = 1f;
            audioSource.volume = 1f;
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
