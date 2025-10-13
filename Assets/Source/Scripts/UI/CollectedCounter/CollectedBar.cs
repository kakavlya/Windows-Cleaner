using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectedBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Scores _scores;
    private readonly float _maxPercentage = 100f;
    [SerializeField] private TMP_Text _hitScore;

    private void Start()
    {
        _slider.maxValue = _maxPercentage;
    }

    private void OnEnable()
    {
        _scores.ProgressUpdated += OnPercentValueChanged;
    }

    private void OnDisable()
    {
        _scores.ProgressUpdated -= OnPercentValueChanged;
    }
    private void OnPercentValueChanged(float currentPercent)
    {
        SetSliderValue(currentPercent);
        SetPercentText(currentPercent);
    }

    private void SetPercentText(float currentPercent)
    {
        _hitScore.text = (currentPercent.ToString("0.#") + "%");
    }

    private void SetSliderValue(float currentPercent)
    {
        _slider.value = currentPercent;
    }
}