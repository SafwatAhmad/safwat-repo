using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace Safwat.ToolKit.Audio
{
    enum SoundType
    {
        bgm,
        pop
    }

    internal class AudioManager : MonoBehaviour
    {
        internal static AudioManager Instance { get; set; }

        const string SFXVolumeKey = "SFXVolume";
        const string BGMVolumeKey = "BGMVolume";

        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Sound[] sounds;
        private Dictionary<SoundType, Sound> soundsDictionary = new();

        internal float SFXVolume
        {
            get => PlayerPrefs.GetFloat(SFXVolumeKey, 1);
            set
            {
                audioMixer.SetFloat(SFXVolumeKey, value);
                PlayerPrefs.SetFloat(SFXVolumeKey, Mathf.Log10(value) * 20);
            }
        }

        internal float BGMVolume
        {
            get => PlayerPrefs.GetFloat(BGMVolumeKey, 1);
            set
            {
                audioMixer.SetFloat(BGMVolumeKey, value);
                PlayerPrefs.SetFloat(BGMVolumeKey, Mathf.Log10(value) * 20);
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                foreach (var s in sounds)
                {
                    if (s.Source == null)
                        s.Source = gameObject.AddComponent<AudioSource>();

                    if (soundsDictionary.TryAdd(s.Type, s) is false)
                        Debug.LogError("Sound " + s.Type + " already exists!");

                    s.ApplySettings();
                }
            }
            else
                Destroy(gameObject);
        }

        internal void PlaySound(SoundType type)
        {
            if (soundsDictionary.TryGetValue(type, out Sound sound))
                sound.Source.Play();
            else
                Debug.LogError("Sound " + type + " not found!");
        }
    }

    [Serializable]
    internal class Sound
    {
        [Space(20)]
        [SerializeField] private string name;
        [SerializeField] private SoundType type;
        [Space(20)]
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioMixerGroup mixerGroup;
        [Space(20)]
        [SerializeField] private bool loop;
        [SerializeField] private bool playOnAwake;
        [Space(20)]
        [SerializeField][Range(0, 1)] private float spatialBlend;

        internal SoundType Type => type;

        internal AudioSource Source
        {
            get => source;
            set => source = value;
        }

        internal void ApplySettings()
        {
            Source.clip = clip;
            Source.outputAudioMixerGroup = mixerGroup;
            Source.playOnAwake = playOnAwake;
            Source.loop = loop;
            Source.spatialBlend = spatialBlend;
        }
    }
}