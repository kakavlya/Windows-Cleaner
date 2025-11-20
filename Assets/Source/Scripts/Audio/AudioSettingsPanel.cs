using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using WindowsCleaner.AudioNs;

namespace WindowsCleaner.AudioNs
{
    public class AudioSettingsPanel : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _sfxToggle;

        private bool _initialized;

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => Audio.Instance != null);

            if (!_initialized)
            {
                InitializeUI();
                _initialized = true;
            }
        }

        private void InitializeUI()
        {
            _musicSlider.value = Audio.Instance.MusicVolume;
            _sfxSlider.value = Audio.Instance.SfxVolume;
            _musicToggle.isOn = Audio.Instance.IsMusicEnabled;
            _sfxToggle.isOn = Audio.Instance.IsSfxEnabled;

            _musicSlider.onValueChanged.AddListener(Audio.Instance.SetMusicVolume);
            _sfxSlider.onValueChanged.AddListener(Audio.Instance.SetSfxVolume);
            _musicToggle.onValueChanged.AddListener(Audio.Instance.ToggleMusic);
            _sfxToggle.onValueChanged.AddListener(Audio.Instance.ToggleSfx);
        }

        private void OnDisable()
        {
            if (Audio.Instance != null)
                Audio.Instance.SaveSettings();
        }

        private void OnDestroy()
        {
            if (Audio.Instance == null)
                return;

            if (_musicSlider != null)
                _musicSlider.onValueChanged.RemoveListener(Audio.Instance.SetMusicVolume);

            if (_sfxSlider != null)
                _sfxSlider.onValueChanged.RemoveListener(Audio.Instance.SetSfxVolume);

            if (_musicToggle != null)
                _musicToggle.onValueChanged.RemoveListener(Audio.Instance.ToggleMusic);

            if (_sfxToggle != null)
                _sfxToggle.onValueChanged.RemoveListener(Audio.Instance.ToggleSfx);
        }
    }
}