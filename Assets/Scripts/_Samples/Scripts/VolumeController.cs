using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;

namespace _Samples.Scripts
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private Volume volume;

        public bool isScary;
        private DigitalGlitchVolume digitalGlitchVolume;
        private AnalogGlitchVolume analogGlitchVolume;
        private ColorAdjustments colorAdjustmentsVolume;

        private void Awake()
        {
            volume.profile.TryGet<ColorAdjustments>(out var outColorAdjustmentsVolume);
            volume.profile.TryGet<DigitalGlitchVolume>(out var outDigitalGlitchVolume);
            volume.profile.TryGet<AnalogGlitchVolume>(out var outAnalogGlitchVolume);

            digitalGlitchVolume = outDigitalGlitchVolume;
            analogGlitchVolume = outAnalogGlitchVolume;
            colorAdjustmentsVolume = outColorAdjustmentsVolume;

            ResetSettings();
        }

        private void Update()
        {
            if (isScary == true)
            {
                digitalGlitchVolume.intensity.value = Mathf.Lerp(digitalGlitchVolume.intensity.value, 0.1f, 0.6f * Time.deltaTime);
                analogGlitchVolume.scanLineJitter.value = Mathf.Lerp(analogGlitchVolume.scanLineJitter.value, 0.34f, 0.6f * Time.deltaTime);
                analogGlitchVolume.verticalJump.value = Mathf.Lerp(analogGlitchVolume.verticalJump.value, 0.193f, 0.6f * Time.deltaTime);
                analogGlitchVolume.horizontalShake.value = Mathf.Lerp(analogGlitchVolume.horizontalShake.value, 0.068f, 0.6f * Time.deltaTime);
                analogGlitchVolume.colorDrift.value = Mathf.Lerp(analogGlitchVolume.colorDrift.value, 0.634f, 0.6f * Time.deltaTime);
                colorAdjustmentsVolume.postExposure.value = Mathf.Lerp(colorAdjustmentsVolume.postExposure.value, -5.03f, 0.3f * Time.deltaTime);
            }
        }

        public void ResetSettings()
        {
            digitalGlitchVolume.intensity.value = 0;

            analogGlitchVolume.scanLineJitter.value = 0;

            analogGlitchVolume.verticalJump.value = 0;

            analogGlitchVolume.horizontalShake.value = 0;
            analogGlitchVolume.colorDrift.value = 0;

            colorAdjustmentsVolume.postExposure.value = 0;
        }
    }
}