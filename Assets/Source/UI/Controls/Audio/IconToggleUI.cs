using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class IconToggleUI : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private GameObject _iconOn;
    [SerializeField] private GameObject _iconOff;

    private void Start()
    {
        UpdateIcons(_toggle.isOn);
        _toggle.onValueChanged.AddListener(UpdateIcons);
    }

    private void UpdateIcons(bool isOn)
    {
        _iconOn.SetActive(isOn);
        _iconOff.SetActive(!isOn);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(UpdateIcons);
    }
}
