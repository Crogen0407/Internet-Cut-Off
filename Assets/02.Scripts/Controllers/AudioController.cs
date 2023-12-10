using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void Play()
    {
        _audioSource.Play();
    }

    public void ChangeClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }
}
