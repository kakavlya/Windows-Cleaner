using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameProgress;

public class GameDataHandle
{
    private const string _gameProgressPref = "GameProgress";
    private string _leaderBoardName = "WindowsCleanerLeaderboard";

    public void SaveProgress(GameProgress progress)
    {
        string json = JsonUtility.ToJson(progress);
        PlayerPrefs.SetString(_gameProgressPref, json);
        PlayerPrefs.Save();
    }

    public GameProgress LoadProgress()
    {
        if (PlayerPrefs.HasKey(_gameProgressPref))
        {
            string json = PlayerPrefs.GetString(_gameProgressPref);
            return JsonUtility.FromJson<GameProgress>(json);
        }
        else
        {
            GameProgress newProgress = new GameProgress();
            newProgress.Levels.Add(new LevelData { LevelNumber = 1, IsUnlocked = true, Score = 0 });
            for (int i = 2; i <= 50; i++)
            {
                newProgress.Levels.Add(new LevelData { LevelNumber = i, IsUnlocked = false, Score = 0 });
            }
            SaveProgress(newProgress);
            return newProgress;
        }
    }

    public void ResetGameProgress()
    {
        DeleteGameProgress();
        LoadProgress();
    }

    private void DeleteGameProgress()
    {
        PlayerPrefs.DeleteKey(_gameProgressPref);
        PlayerPrefs.Save();
    }
}
