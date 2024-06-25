using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioPlayer;

    public void playClip(AudioClip clip) { 
        GameObject apCopy = Instantiate(audioPlayer, this.gameObject.transform);
        AudioSource apSource = apCopy.GetComponent<AudioSource>();

        apSource.clip = clip;
        apSource.Play();
    }
}
