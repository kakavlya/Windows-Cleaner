using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameProgress;

[DefaultExecutionOrder(-1000)]
public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    public event Action<int> OnLevelChanged;
    public int CurrentLevel { get; private set; }

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
        _gameDataHandle.ResetGameProgress();
        GameProgress progress = _gameDataHandle.LoadProgress();
        CurrentLevel = progress.CurrentLevel > 0 ? progress.CurrentLevel : 1;
        NotifyLevelChanged();
    }

    public void CompleteLevel(float newScore)
    {
        GameProgress progress = _gameDataHandle.LoadProgress();
        LevelData currentLevelData = progress.Levels.Find(level => level.LevelNumber == CurrentLevel);
        if (currentLevelData == null)
            throw new Exception($"Level {CurrentLevel} not found in progress data.");

        if (newScore > currentLevelData.Score)
        {
            currentLevelData.Score = newScore;
        }

        progress.UpdateTotalScore();
        progress.CurrentLevel = CurrentLevel;

        LevelData nextLevel = progress.Levels.Find(level => level.LevelNumber == CurrentLevel + 1);
        if (nextLevel != null && !nextLevel.IsUnlocked)
        {
            nextLevel.IsUnlocked = true;
        }

        _gameDataHandle.SaveProgress(progress);
    }


    public void SetLevel(int newLevel)
    {
        CurrentLevel = newLevel;
        NotifyLevelChanged();
    }

    public void NextLevel()
    {
        SetLevel(CurrentLevel + 1);
    }

    private void NotifyLevelChanged()
    {
        Debug.Log("Level Changed: " + CurrentLevel);
        OnLevelChanged?.Invoke(CurrentLevel);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
