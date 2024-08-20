using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> audioClips;
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Update() {
        if (!audioSource.isPlaying) {
            PlayRandomClip();
        }
    }

    private void PlayRandomClip() {
        audioSource.Stop();
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
    }
}
