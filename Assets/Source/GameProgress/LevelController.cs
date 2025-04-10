using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameProgress;

[DefaultExecutionOrder(-1000)]
public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    public event Action<int> OnLevelChanged;
    public int CurrentLevelInController { get; private set; }
    public bool IsRestartingLevel { get; private set; } = false;
    private GameDataHandle _gameDataHandle = new GameDataHandle();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // uncomment to reset progress
        //_gameDataHandle.ResetGameProgress();
        GameProgress progress = _gameDataHandle.LoadProgress();
        CurrentLevelInController = progress.CurrentLevel > 0 ? progress.CurrentLevel : 1;
        NotifyLevelChanged();
    }

    private void PrintAllLevelsDebug(GameProgress progress)
    {
        foreach(var level in progress.Levels)
        {
            Debug.Log($"level number: {level.LevelNumber} level is unlocked: {level.IsUnlocked}");
        }
    }

    public void CompleteLevel(float newScore)
    {
        GameProgress progress = _gameDataHandle.LoadProgress();
        LevelData currentLevelData = progress.Levels.Find(level => level.LevelNumber == CurrentLevelInController);
        if (currentLevelData == null)
            throw new Exception($"Level {CurrentLevelInController} not found in progress data.");

        if (newScore > currentLevelData.Score)
        {
            currentLevelData.Score = newScore;
        }

        progress.UpdateTotalScore();
        progress.CurrentLevel = CurrentLevelInController;

        LevelData nextLevel = progress.Levels.FirstOrDefault(level => level.LevelNumber == (progress.CurrentLevel + 1));

        if (nextLevel != null && !nextLevel.IsUnlocked)
        {
            nextLevel.IsUnlocked = true;  
        }

        _gameDataHandle.SaveProgress(progress);
    }


    public void SetLevel(int newLevel)
    {
        CurrentLevelInController = newLevel;
        NotifyLevelChanged();
    }

    public void NextLevel()
    {
        GoingNextLevel();
        SetLevel(CurrentLevelInController + 1);
    }

    private void NotifyLevelChanged()
    {
        OnLevelChanged?.Invoke(CurrentLevelInController);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartingLevel()
    {
        IsRestartingLevel = true;
    }

    public void GoingNextLevel()
    {
        IsRestartingLevel = false;
    }
}
