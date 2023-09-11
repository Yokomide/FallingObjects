using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewAudioPreset", menuName = "Custom/AudioPreset")]

public class SoundPreset : ScriptableObject
{
    public AudioClip BGM;

    public AudioClip correctSound;
    public AudioClip badSound;
    public AudioClip clickSound;

}
