using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsPanel : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private void OnEnable()
    {
        TryConnectToAudio();
    }

    private void TryConnectToAudio()
    {
        if (Audio.Instance == null)
        {
            Debug.LogWarning("Audio.Instance is missing — UI will not bind.");
            return;
        }

        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
        musicToggle.onValueChanged.RemoveAllListeners();
        sfxToggle.onValueChanged.RemoveAllListeners();

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
}
