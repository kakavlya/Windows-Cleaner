using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using static GameProgress;

public class Level : MonoBehaviour
{
    private const string _gameProgressPref = "GameProgress";
    [SerializeField] private string _leaderBoardName = "WindowsCleanerLeaderboard";
    private GameDataHandle _gameDataHandle;
    private void Awake()
    {
        _gameDataHandle = new GameDataHandle();
    }

    public void CompleteLevel(int currentLevelNumber, float newScore)
    {
        GameProgress progress = _gameDataHandle.LoadProgress();

        // Находим текущий уровень по номеру
        LevelData currentLevel = progress.Levels.Find(level => level.LevelNumber == currentLevelNumber);

        if (currentLevel != null)
        {
            if (newScore > currentLevel.Score)
            {
                currentLevel.Score = newScore;
                progress.UpdateTotalScore();

            }

            LevelData nextLevel = progress.Levels.Find(level => level.LevelNumber == currentLevelNumber + 1);
            if (nextLevel != null && !nextLevel.IsUnlocked)
            {
                nextLevel.IsUnlocked = true;
            }

            _gameDataHandle.SaveProgress(progress);
        }

        UpdateLeaderBoard(Mathf.RoundToInt(progress.TotalScore));
    }

    public void OnLevelCompleted(int levelNumber, int score)
    {
        CompleteLevel(levelNumber, score);
        LoadLevel(levelNumber+1);
    }

    public void OnLevelCompleted(int levelNumber)
    {
        LoadLevel(levelNumber + 1);
    }

    private void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + (levelIndex));
        //TODO запускать скрипт сцены, с определенным номером левела
        //запуск через и регулировка уровня через animation curve, в зависимости от прогрессии
    }

    private void UpdateLeaderBoard(int newRecord)
    {
        YandexGame.NewLeaderboardScores(_leaderBoardName, newRecord);
    }
}
