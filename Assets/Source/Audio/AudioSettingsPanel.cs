using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class AudioSettingsPanel : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private bool _initialized = false;

    private IEnumerator Start()
    {
        //while (Audio.Instance == null)
        //    yield return null;
        yield return new WaitUntil(() => Audio.Instance!= null);

        if (!_initialized)
        {
            InitializeUI();
            _initialized = true;
        }
    }

    private void InitializeUI()
    {
        musicSlider.value = Audio.Instance.musicVolume;
        sfxSlider.value = Audio.Instance.sfxVolume;
        musicToggle.isOn = Audio.Instance.IsMusicEnabled;
        sfxToggle.isOn = Audio.Instance.IsSfxEnabled;

        musicSlider.onValueChanged.AddListener(Audio.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(Audio.Instance.SetSfxVolume);
        musicToggle.onValueChanged.AddListener(Audio.Instance.ToggleMusic);
        sfxToggle.onValueChanged.AddListener(Audio.Instance.ToggleSfx);
    }

    private void OnDisable()
    {
        if (Audio.Instance != null)
        {
            Audio.Instance.SaveSettings();
        }
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(Audio.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(Audio.Instance.SetSfxVolume);
        musicToggle.onValueChanged.RemoveListener(Audio.Instance.ToggleMusic);
        sfxToggle.onValueChanged.RemoveListener(Audio.Instance.ToggleSfx);
    }

    //private void OnEnable()
    //{

    //    InitializeUI();
    //    IconToggleUI[] iconToggles = GetComponentsInChildren<IconToggleUI>();
    //}
}
