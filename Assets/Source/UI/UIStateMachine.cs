using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UIStateMachine : MonoBehaviour
{

    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameObject _tutorialScreen;
    [SerializeField] private GameObject _touchControlsScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private EndLevelScreen _endLevelScreen;
    [SerializeField] private GameObject _sliderPickedBar;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private CollectedFinisher _collectedFinisher;
    [SerializeField] private GameObject _sideButtons;
    [SerializeField] private GameObject _audioSettingsPanel;
    [SerializeField] private GameObject _leaderBoardPanel;
    [SerializeField] private GameObject _levelSelectionPanel;

    private UIState _currentState;

    private void Start()
    {
        SwitchState(UIState.StartScreen);
    }

    public void SwitchState(UIState newState)
    {
        _currentState = newState;
        DisableAll();
        switch (newState)
        {
            case UIState.StartScreen:
                _startScreen.gameObject.SetActive(true);
                _sideButtons.SetActive(true);
                break;
            case UIState.Playing:
                _touchControlsScreen.SetActive(true);
                _sliderPickedBar.SetActive(true);
                _sideButtons.SetActive(true);
                break;
            case UIState.GameOver:
                _gameOverScreen.gameObject.SetActive(true);
                break;
            case UIState.PauseMenu:
                _pausePanel.SetActive(true);
                break;
            case UIState.EndLevelAnimation:
                break;
            case UIState.EndLevel:
                _endLevelScreen.gameObject.SetActive(true);
                break;
        }
    }
    
    public UIState GetCurrentState()
    {
        return _currentState;
    }

    private void DisableAll()
    {
        _startScreen.gameObject.SetActive(false);
        _tutorialScreen.SetActive(false);
        _touchControlsScreen.SetActive(false);
        _gameOverScreen.gameObject.SetActive(false);
        _endLevelScreen.gameObject.SetActive(false);
        _sliderPickedBar.SetActive(false);
        _pausePanel.SetActive(false);
        _sideButtons.SetActive(false);
        _audioSettingsPanel.SetActive(false);
        _leaderBoardPanel.SetActive(false);
        _levelSelectionPanel.SetActive(false);
    }
}
