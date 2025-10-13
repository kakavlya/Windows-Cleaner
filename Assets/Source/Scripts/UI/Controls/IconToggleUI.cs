using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    private void OnEnable()
    {
        Debug.Log("IconToggleUI גûחמג OnEnabled");

        _toggle.onValueChanged.AddListener(UpdateIcons);
        UpdateIcons(_toggle.isOn);
    }

    private void OnDisable()
    {
        Debug.Log("IconToggleUI גûחמג OnDisable");
        _toggle.onValueChanged.RemoveListener(UpdateIcons);
    }

    private void UpdateIcons(bool isOn)
    {
        _iconOn.SetActive(isOn);
        _iconOff.SetActive(!isOn);
        Debug.Log($"IconToggle UIUpdateIcons");
        //Debug.Log($"_iconOn is: {_iconOn}");
        //Debug.Log($"_iconOff is: {_iconOff}");
    }
}
