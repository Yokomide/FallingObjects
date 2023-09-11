using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip correctSFX, wrongSFX, compresssedCorrestSFX;
    private AudioClip _currentCorrectSFX;

    private void Start()
    {
        SetNormalSFX();
    }
    public void PlayCorrectSound()
    {
        audioSource.clip = _currentCorrectSFX;
        audioSource.Play();
    }

    public void PlayWrongSound()
    {
        audioSource.clip = wrongSFX;
        audioSource.Play();
    }

    public void SetNormalSFX()
    {
        audioSource.clip = correctSFX;
        _currentCorrectSFX = correctSFX;
    }

    public void SetCompressedSFX()
    {
        audioSource.clip = compresssedCorrestSFX;
        _currentCorrectSFX = compresssedCorrestSFX;
    }
}