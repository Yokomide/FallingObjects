using _Samples.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSetter : MonoBehaviour
{

    [SerializeField] private CharacterAnimationController _characterAnimationController;
    [SerializeField] private CharacterAnimationController _uiCharacterAnimationController;

    [SerializeField] private AudioController _audioController;
    [SerializeField] private VolumeController _volumeController;

    [SerializeField] private List<SkinPreset> _presets = new List<SkinPreset>();

    private void Start()
    {
        StartCoroutine(InstanceWaiter());
    }
    public void ChangeSkin(int skinID)
    {
        _characterAnimationController.SetAnimatorController(_presets[skinID].AnimationController);
        _uiCharacterAnimationController.SetAnimatorController(_presets[skinID].UIAnimationController);
        _audioController.SetPreset(_presets[skinID].soundPreset);
        _volumeController.SetVolume(_presets[skinID].volumeProfile);
        GameManager.instance.PlayerInfo.currentSkin = skinID;
        GameManager.instance.Save();
    }
    private IEnumerator InstanceWaiter()
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        ChangeSkin(GameManager.instance.PlayerInfo.currentSkin);
    }
}
