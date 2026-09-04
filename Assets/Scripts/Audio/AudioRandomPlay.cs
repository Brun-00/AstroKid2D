using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomPlay : MonoBehaviour
{
    public List<AudioClip> audioClips;

    public List<AudioSource> audioSources;

    private int _index = 0;

    public void PlayRandom()
    {
        // Reuse the audio sources once the list reaches its end.
        if (_index >= audioSources.Count)
            _index = 0;

        var audioSource = audioSources[_index];

        // Pick a random clip and play it on the current source.
        audioSource.clip = audioClips[
            Random.Range(0, audioClips.Count)
        ];

        audioSource.Play();

        _index++;
    }
}