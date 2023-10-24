using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Safwat.Essentials.Audio
{
    [CreateAssetMenu(fileName = "NewMixerGroupProfile", menuName = "AudioMixer Profile/NewProfile")]
    public class MixerGroupProfile : ScriptableObject
    {
        [field: SerializeField] public AudioMixerGroup MixerGroup { get; private set; }
        [field: SerializeField] public ProfileGroup ProfileGroup { get; private set; } = default;
        [field: SerializeField] public AudioMixer Mixer { get; private set; }

        [Header("Parameters:\n(Copy exact parameter name key and\n set parameter to expose in mixer)")]
        #region Volume
        [Header("Volume")]
        [SerializeField] private string volumeKey;
        [field: SerializeField, Range(0.1f, 1f)] public float MaxVolume { get; private set; } = 1;

        public float Volume
        {
            get => PlayerPrefs.GetFloat(volumeKey, MaxVolume);
            set
            {
                value = Mathf.Clamp(value, 0, MaxVolume);
                PlayerPrefs.SetFloat(volumeKey, value);
                SetMixerVolume(value);
            }
        }

        private void SetMixerVolume(float value) => Mixer.SetFloat(volumeKey, UnitRangeToDecibell(value));

        private float UnitRangeToDecibell(float value) => Mathf.Lerp(-80, 0, value);
        #endregion

        public void Initialize() => SetMixerVolume(Volume);

        private void OnValidate()
        {
            Mixer = MixerGroup.audioMixer;
            volumeKey = $"{MixerGroup.name}Volume";
            ProfileGroup = GeneralUtilities.StringToEnum<ProfileGroup>(MixerGroup.name);
        }
    }
}