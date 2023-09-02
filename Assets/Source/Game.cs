using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private EndLevelScreen _endLevelScreen;
    [SerializeField] private Player _player;
    [SerializeField] private CollectedFinisher _collectedFinisher;
    [SerializeField] private float _totalSequenceDuration;
    [SerializeField] private CinemachineVirtualCamera _secondCam;
    [SerializeField] private int _secondCamPriority;

    private void Start()
    {
        _startScreen.gameObject.SetActive(true);
        PauseGame();
    }

    private void OnEnable()
    {
        _startScreen.StartButtonClick += OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _endLevelScreen.RestartButtonClick += OnRestartButtonClick;
        _player.GameOver += GameOver;
        _player.WonLevel += WonLevel;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClick -= OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _endLevelScreen.RestartButtonClick -= OnRestartButtonClick;
        _player.GameOver -= GameOver;
        _player.WonLevel -= WonLevel;
    }

    private void StartGame()
    {
        Time.timeScale = 1;

        // TODO remove later
        CheckCoinsScene();
    }

    private void CheckCoinsScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        // Check if the name of the current Active Scene is your first Scene.
        if (scene.name == "TestFullSequence")
        {
            WonLevel();
        }
    }

    private void OnPlayButtonClick()
    {
        _startScreen.gameObject.SetActive(false);
        StartGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void GameOver()
    {
        PauseGame();
        _gameOverScreen.gameObject.SetActive(true);
    }

    private void WonLevel()
    {
        _collectedFinisher.StartFinishingSequence();
        Invoke(nameof(ShowMenuAndPauseGame), _totalSequenceDuration);
        _secondCam.Priority = _secondCamPriority;
    }

    private void ShowMenuAndPauseGame()
    {
        PauseGame();
        _endLevelScreen.gameObject.SetActive(true);
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
