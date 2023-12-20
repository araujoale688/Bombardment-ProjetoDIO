using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundOnAwake : MonoBehaviour
{
    public List<AudioClip> audioClips;

    private AudioSource audio_Source;

    private void Awake()
    {
        audio_Source = GetComponent<AudioSource>();
    }

    void Start()
    {
        AudioClip clip = audioClips[Random.Range(0, audioClips.Count)];

        audio_Source.PlayOneShot(clip);
    }
}