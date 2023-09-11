using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] List<SoundPreset> _soundPresetList = new List<SoundPreset>();

    [SerializeField]
    private AudioSource _sfxAudioSource;

    [SerializeField]
    private AudioSource _bgmAudioSource;

    private SoundPreset _currentSoundPreset;


    public void SetPreset(SoundPreset soundPreset)
    {
        _currentSoundPreset = soundPreset;
        SetBGM();
    }
    public void SetBGM()
    {
        _bgmAudioSource.clip = _currentSoundPreset.BGM;
        _bgmAudioSource.Play();
    }
    public void PlayCorrectSound()
    {
        _sfxAudioSource.PlayOneShot(_currentSoundPreset.correctSound);
    }

    public void PlayBadSound()
    {
        _sfxAudioSource.PlayOneShot(_currentSoundPreset.badSound);
    }

    public void PlayClickSound()
    {
        _sfxAudioSource.PlayOneShot(_currentSoundPreset.clickSound);
    }
}