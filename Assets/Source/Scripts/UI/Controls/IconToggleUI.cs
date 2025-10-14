using UnityEngine;
using UnityEngine.UI;

namespace WindowsCleaner.UI
{
    public class IconToggleUI : MonoBehaviour
    {
        private Toggle _toggle;
        [SerializeField] private GameObject _iconOn;
        [SerializeField] private GameObject _iconOff;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(UpdateIcons);
            UpdateIcons(_toggle.isOn);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(UpdateIcons);
        }

        private void UpdateIcons(bool isOn)
        {
            _iconOn.SetActive(isOn);
            _iconOff.SetActive(!isOn);
        }
    }
}