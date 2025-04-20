using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private UIStateMachine _uiStateMachine;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
    }

    private void OnResumeButtonClick()
    {
        _uiStateMachine.SwitchState(UIState.Playing);
    }
}
