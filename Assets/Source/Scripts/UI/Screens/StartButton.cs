using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private LevelSelector _levelSelector;
    private GameDataHandle _gameDataHandle;

    private void Start()
    {
        _startButton.onClick.AddListener(LoadLevel);
        _gameDataHandle = new GameDataHandle();
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadLevel);
    }

    private void LoadLevel()
    {
        int minimalLevelToLoad = 1;

        var progress = _gameDataHandle.LoadProgress();
        var levels = progress.Levels;
        int highestUnlockedLevel = progress.Levels
            .Where(level => level.IsUnlocked)
            .Select(level => level.LevelNumber)
            .DefaultIfEmpty(minimalLevelToLoad)
            .Max();

        _levelSelector.LoadLevel(highestUnlockedLevel);
    }
}
