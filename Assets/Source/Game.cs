using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    [SerializeField] private StartScreen _startScreen;

    private void Start()
    {
        _startScreen.gameObject.SetActive(true);
        PauseGame();
    }

    private void OnEnable()
    {
        _startScreen.StartButtonClick += OnPlayButtonClick;
    }

    private void OnDisable()
    {
        _startScreen.StartButtonClick -= OnPlayButtonClick;
    }

    private void StartGame()
    {
        Time.timeScale = 1;
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
        // TODO open gameOver screen
    }

    private void GameWon()
    {
        PauseGame();
        // TODO open gameWon screen
    }
}
