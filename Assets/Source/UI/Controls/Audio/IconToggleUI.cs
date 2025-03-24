using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class IconToggleUI : MonoBehaviour
{
    private Toggle _toggle;
    [SerializeField] private GameObject _iconOn;
    [SerializeField] private GameObject _iconOff;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();

        if (_iconOn == null || _iconOff == null)
            Debug.LogWarning($"{name}: Icons are not set");
    }

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(UpdateIcons);
        UpdateIcons(_toggle.isOn);
    }

    public void ForceUpdate()
    {
        _toggle = GetComponent<Toggle>();
        UpdateIcons(_toggle.isOn);
    }

    private void UpdateIcons(bool isOn)
    {
        _iconOn.SetActive(isOn);
        _iconOff.SetActive(!isOn);
        Debug.Log($"(bool isOn is: {isOn}");
        Debug.Log($"_iconOn is: {_iconOn}");
        Debug.Log($"_iconOff is: {_iconOff}");
    }

    private void OnEnable()
    {
        _toggle = GetComponent<Toggle>();
        Debug.Log("IconToggleUI גûחמג OnEnabled");
        _toggle.onValueChanged.RemoveListener(UpdateIcons);
        _toggle.onValueChanged.AddListener(UpdateIcons);
        ForceUpdate();
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(UpdateIcons);
    }

}
