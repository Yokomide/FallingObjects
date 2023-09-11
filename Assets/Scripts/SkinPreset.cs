using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "NewAudioPreset", menuName = "Custom/SkinPreset")]
public class SkinPreset : ScriptableObject
{
    public RuntimeAnimatorController AnimationController;
    public RuntimeAnimatorController UIAnimationController;

    public SoundPreset soundPreset;
    public VolumeProfile volumeProfile;

}
