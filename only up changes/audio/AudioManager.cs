using UnityEngine;

namespace Safwat.Essentials.Audio
{   // Refactor per game basis
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        [SerializeField] private AudioMixerController mixerController;

        [Header("Bgm")]
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private AudioClip[] bgMusics;

        [Header("Sfx")]
        [SerializeField] private AudioSource sfxAudioSource;
        [SerializeField] private AudioSource collectablesAudioSource;
        [SerializeField] private AudioClip clickSound;
        [SerializeField] private AudioClip panelOpenSound;
        [SerializeField] private AudioClip panelCloseSound;
        [SerializeField] private AudioClip collectablesSound;
        [SerializeField] private int maxBgmFragments = 10;

        #region MonoBehaviour
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Start() => PlayBgm();
        #endregion

        #region Bgm
        public bool IsBgmMute => BgmVolume == 0;

        public float BgmVolume
        {
            get => mixerController.GetVolume(ProfileGroup.Bgm);
            set => mixerController.SetVolume(ProfileGroup.Bgm, value);
        }

        public int BgmsUnlocked => (BgmFragmentsCollected / maxBgmFragments) + 1;    // 1 => pre-unlocked

        public bool AllBgmsUnlocked => BgmsUnlocked >= bgMusics.Length - 1;

        public int BgmFragments => BgmFragmentsCollected % maxBgmFragments;

        public int MaxBgmFragments => maxBgmFragments;

        public int CurrentBgm
        {
            get => PlayerPrefs.GetInt("CurrentBgm", 0);
            set => PlayerPrefs.SetInt("CurrentBgm", value >= bgMusics.Length ? 0 : value);
        }

        public int BgmFragmentsCollected
        {
            get => PlayerPrefs.GetInt("BgmFragmentsCollected", 0);
            set
            {
                if (AllBgmsUnlocked) return;
                PlayerPrefs.SetInt("BgmFragmentsCollected", value);
            }
        }

        public void PlayNextBgm()
        {
            CurrentBgm = CurrentBgm + 1 > BgmsUnlocked ? 0 : CurrentBgm + 1;
            PlayBgm();
        }

        public void PlayNewBgm()
        {
            CurrentBgm = BgmsUnlocked;
            PlayBgm();
        }

        private void PlayBgm()
        {
            bgmAudioSource.Stop();
            bgmAudioSource.clip = bgMusics[CurrentBgm];
            bgmAudioSource.Play();
        }
        #endregion

        #region Sfx
        public bool IsSfxMute => SfxVolume == 0;

        public float SfxVolume
        {
            get => mixerController.GetVolume(ProfileGroup.Sfx);
            set => mixerController.SetVolume(ProfileGroup.Sfx, value);
        }

        public void PlayClickSound() => sfxAudioSource.PlayOneShot(clickSound);

        public void PlayPanelOpenSound() => sfxAudioSource.PlayOneShot(panelOpenSound);

        public void PlayPanelCloseSound() => sfxAudioSource.PlayOneShot(panelCloseSound);

        public void PlayCollectSound() => collectablesAudioSource.PlayOneShot(collectablesSound);
        #endregion
    }
}