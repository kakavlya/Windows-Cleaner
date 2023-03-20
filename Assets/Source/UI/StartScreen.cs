using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button _button;
    public event UnityAction StartButtonClick;

    private void OnButtonClick()
    {
        StartButtonClick?.Invoke();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}
