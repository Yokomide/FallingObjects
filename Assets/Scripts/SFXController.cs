using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip correctSFX, wrongSFX;

    public void PlayCorrectSound()
    {
        audioSource.clip = correctSFX;
        audioSource.Play();
    }

    public void PlayWrongSound()
    {
        audioSource.clip = wrongSFX;
        audioSource.Play();
    }
}