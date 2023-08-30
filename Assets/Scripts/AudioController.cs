using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgmSFX, glitchSFX;

    public void PlayNormalSound()
    {
        audioSource.clip = bgmSFX;
        audioSource.Play();
    }

    public void PlayHorrorSound()
    {
        audioSource.clip = glitchSFX;
        audioSource.Play();
    }
}