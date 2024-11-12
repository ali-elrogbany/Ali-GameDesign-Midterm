using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip correctSfx;
    [SerializeField] private AudioClip incorrectSfx;
    [SerializeField] private AudioClip timeOverSfx;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public void PlayCorrectSfx()
    {
        sfxAudioSource.PlayOneShot(correctSfx);
    }

    public void PlayIncorrectSfx()
    {
        sfxAudioSource.PlayOneShot(incorrectSfx);
    }

    public void PlayTimeOverSfx()
    {
        sfxAudioSource.ignoreListenerPause = true;
        sfxAudioSource.PlayOneShot(timeOverSfx);
    }
}
