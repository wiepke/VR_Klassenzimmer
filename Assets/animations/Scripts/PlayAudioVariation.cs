using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioVariation : StateMachineBehaviour
{
    public AudioClip[] MaleVariations;
    public float MaleVoiceVolume = 1.0f;
    public AudioClip[] FemaleVariations;
    public float FemaleVoiceVolume = 1.0f;

    private AudioSource source;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source = animator.transform.GetComponentInParent<AudioSource>();
        source.enabled = true;

        animator.gameObject.GetComponent<StudentAnimationEvents>().CurrentSMB = this;
    }

    // Will be called from a animation event by a MonoBehaviour method
    public void PlayAudio(bool variant)
    {
        AudioClip[] clips = variant ? MaleVariations : FemaleVariations;
        AudioClip clip = clips[Random.Range(0, clips.Length - 1)];

        float volume = variant ? MaleVoiceVolume : FemaleVoiceVolume;
        source.PlayOneShot(clip, volume);
    }

    // TODO try out
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        source.enabled = false;
    }
}
