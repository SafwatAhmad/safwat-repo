using UnityEngine;
using System.Collections.Generic;

namespace Safwat.Essentials.Audio
{
    public enum ProfileGroup { /* 0 => default */Bgm = 1, Sfx = 2 }

    public class AudioMixerController : MonoBehaviour
    {
        [SerializeField] private MixerGroupProfile[] profiles;
        private Dictionary<ProfileGroup, MixerGroupProfile> savedProfiles = new();

        public void Start()
        {
            foreach (MixerGroupProfile profile in profiles)
            {
                profile.Initialize();
                if (!savedProfiles.TryAdd(profile.ProfileGroup, profile))
                    throw new("Avoid duplication and verify Profile enum");
            }
        }

        public float GetVolume(ProfileGroup profile) => savedProfiles[profile].Volume;

        public void SetVolume(ProfileGroup profile, float value) => savedProfiles[profile].Volume = value;
    }
}