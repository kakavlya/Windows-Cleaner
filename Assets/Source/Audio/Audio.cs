using UnityEngine;
using YG;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip _musicClip;

    [Header("Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [SerializeField] private bool _isMusicEnabled = true;
    [SerializeField] private bool _isSfxEnabled = true;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    public bool IsMusicEnabled { get => _isMusicEnabled; private set => _isMusicEnabled = value; }
    public bool IsSfxEnabled { get => _isSfxEnabled; private set => _isSfxEnabled = value; }

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

        // loading local settings
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
            return;

        _musicSource.clip = music;
        _musicSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        ApplySettings();
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
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

    private void ApplySettings()
    {
        if (_musicSource != null)
        {
            _musicSource.mute = !IsMusicEnabled;
            _musicSource.volume = musicVolume;
        }

        if (_sfxSource != null)
        {
            _sfxSource.mute = !IsSfxEnabled;
            _sfxSource.volume = sfxVolume;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.SetInt("MusicEnabled", IsMusicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("SfxEnabled", IsSfxEnabled ? 1 : 0);
        PlayerPrefs.Save();

        // ќбновл€ем объект сохранений дл€ яндекса
        if (YandexGame.SDKEnabled)
        {
            YandexGame.savesData.musicVolume = musicVolume;
            YandexGame.savesData.sfxVolume = sfxVolume;
            YandexGame.savesData.isMusicEnabled = IsMusicEnabled;
            YandexGame.savesData.isSfxEnabled = IsSfxEnabled;
            YandexGame.SaveProgress();
        }
    }

    public void LoadSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
        IsMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        IsSfxEnabled = PlayerPrefs.GetInt("SfxEnabled", 1) == 1;
    }

    public void LoadSettingsFromYandex()
    {
        musicVolume = YandexGame.savesData.musicVolume;
        sfxVolume = YandexGame.savesData.sfxVolume;
        IsMusicEnabled = YandexGame.savesData.isMusicEnabled;
        IsSfxEnabled = YandexGame.savesData.isSfxEnabled;
    }

    public void PlaySfx(AudioClip clip)
    {
        if (IsSfxEnabled && clip != null && _sfxSource != null)
        {
            _sfxSource.PlayOneShot(clip, sfxVolume);
        }
    }
}
