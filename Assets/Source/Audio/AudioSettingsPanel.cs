using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioSettingsPanel : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private bool _initialized = false;

    private IEnumerator Start()
    {
        while (Audio.Instance == null)
            yield return null;

        if (!_initialized)
        {
            InitializeUI();
            _initialized = true;
        }
    }

    private void InitializeUI()
    {
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
        musicToggle.onValueChanged.RemoveAllListeners();
        sfxToggle.onValueChanged.RemoveAllListeners();

        musicSlider.SetValueWithoutNotify(Audio.Instance.musicVolume);
        sfxSlider.SetValueWithoutNotify(Audio.Instance.sfxVolume);
        musicToggle.SetIsOnWithoutNotify(Audio.Instance.IsMusicEnabled);
        sfxToggle.SetIsOnWithoutNotify(Audio.Instance.IsSfxEnabled);

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

    private void OnEnable()
    {
        
        InitializeUI();
        IconToggleUI[] iconToggles = GetComponentsInChildren<IconToggleUI>();
        foreach (var iconToggle in iconToggles)
        {
            //Debug.Log($"Вызван AudioSettingsPanel.OnEnable() iconToggle: {iconToggle}");
            iconToggle.ForceUpdate();
        }
    }
}
