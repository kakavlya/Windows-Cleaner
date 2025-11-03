using UnityEngine;
using YG;

namespace WindowsCleaner.AudioNs
{
    public class Audio : MonoBehaviour
    {
        private static Audio _instance;

        [Header("Audio Clip")]
        [SerializeField] private AudioClip _musicClip;

        [Header("Settings")]
        [Range(0f, 1f)]
        [SerializeField] private float _musicVolume = 1f;
        [Range(0f, 1f)]
        [SerializeField] private float _sfxVolume = 1f;
        [SerializeField] private bool _isMusicEnabled = true;
        [SerializeField] private bool _isSfxEnabled = true;

        private AudioSource _musicSource;
        private AudioSource _sfxSource;

        public static Audio Instance
        {
            get => _instance;
            private set => _instance = value;
        }

        public bool IsMusicEnabled
        {
            get => _isMusicEnabled;
            private set => _isMusicEnabled = value;
        }

        public bool IsSfxEnabled
        {
            get => _isSfxEnabled;
            private set => _isSfxEnabled = value;
        }

        public float MusicVolume
        {
            get => _musicVolume;
            private set => _musicVolume = value;
        }

        public float SfxVolume
        {
            get => _sfxVolume;
            private set => _sfxVolume = value;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;

            _sfxSource = gameObject.AddComponent<AudioSource>();
            _sfxSource.loop = false;
            _sfxSource.playOnAwake = false;

            LoadSettings();

            if (YandexGame.SDKEnabled)
            {
                LoadSettingsFromYandex();
            }

            ApplySettings();
            PlayMusic(_musicClip);
        }

        public void PlayMusic(AudioClip music)
        {
            if (music == null || _musicSource == null || !IsMusicEnabled)
            {
                return;
            }

            _musicSource.clip = music;
            _musicSource.Play();
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            ApplySettings();
        }

        public void SetSfxVolume(float volume)
        {
            SfxVolume = volume;
            ApplySettings();
        }

        public void ToggleMusic(bool enabled)
        {
            IsMusicEnabled = enabled;
            ApplySettings();
        }

        public void ToggleSfx(bool enabled)
        {
            IsSfxEnabled = enabled;
            ApplySettings();
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetFloat("_musicVolume", MusicVolume);
            PlayerPrefs.SetFloat("_sfxVolume", SfxVolume);
            PlayerPrefs.SetInt("MusicEnabled", IsMusicEnabled ? 1 : 0);
            PlayerPrefs.SetInt("SfxEnabled", IsSfxEnabled ? 1 : 0);
            PlayerPrefs.Save();

            if (YandexGame.SDKEnabled)
            {
                YandexGame.savesData.musicVolume = MusicVolume;
                YandexGame.savesData.sfxVolume = SfxVolume;
                YandexGame.savesData.isMusicEnabled = IsMusicEnabled;
                YandexGame.savesData.isSfxEnabled = IsSfxEnabled;
                YandexGame.SaveProgress();
            }
        }

        public void LoadSettings()
        {
            MusicVolume = PlayerPrefs.GetFloat("_musicVolume", 1f);
            SfxVolume = PlayerPrefs.GetFloat("_sfxVolume", 1f);
            IsMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
            IsSfxEnabled = PlayerPrefs.GetInt("SfxEnabled", 1) == 1;
        }

        public void LoadSettingsFromYandex()
        {
            MusicVolume = YandexGame.savesData.musicVolume;
            SfxVolume = YandexGame.savesData.sfxVolume;
            IsMusicEnabled = YandexGame.savesData.isMusicEnabled;
            IsSfxEnabled = YandexGame.savesData.isSfxEnabled;
        }

        public void PlaySfx(AudioClip clip)
        {
            if (IsSfxEnabled && clip != null && _sfxSource != null)
            {
                _sfxSource.PlayOneShot(clip, SfxVolume);
            }
        }

        private void ApplySettings()
        {
            if (_musicSource != null)
            {
                _musicSource.mute = !IsMusicEnabled;
                _musicSource.volume = MusicVolume;
            }

            if (_sfxSource != null)
            {
                _sfxSource.mute = !IsSfxEnabled;
                _sfxSource.volume = SfxVolume;
            }
        }
    }
}