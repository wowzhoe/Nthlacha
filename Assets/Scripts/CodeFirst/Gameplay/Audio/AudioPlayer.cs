﻿using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audio;

    private static float vol = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudio(int id)
    {
        audioSource.PlayOneShot(audio[id]);
    }

    public void PlayAudio(int id, float vol)
    {
        audioSource.PlayOneShot(audio[id], vol);
    }
}