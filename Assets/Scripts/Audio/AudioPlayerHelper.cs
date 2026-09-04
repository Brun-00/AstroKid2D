using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerHelper : MonoBehaviour
{
    public AudioSource audioSource;

    public void Play()
    {
        // Play the assigned audio source.
        audioSource.Play();
    }
}