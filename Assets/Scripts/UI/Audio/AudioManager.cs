using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioPlayer;

    public void playClip(AudioClip clip) { 
        // Create a new audio player object, which will contain a new AudioSource.
        GameObject apCopy = Instantiate(audioPlayer, this.gameObject.transform);
        AudioSource apSource = apCopy.GetComponent<AudioSource>();

        // Place the clip in the audio player's source, and play it.
        apSource.clip = clip;
        apSource.Play();
    }
}
