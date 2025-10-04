using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;
using static GameProgress;

public class GameDataHandle
{
    private const string _gameProgressPref = "GameProgress";
    private readonly string _leaderBoardName = "WindowsCleanerLeaderboard";
    private const int ExpectedLevelsCount = 50;
    public void SaveProgress(GameProgress progress)
    {
        string json = JsonUtility.ToJson(progress);
        PlayerPrefs.SetString(_gameProgressPref, json);
        PlayerPrefs.Save();

        if(YandexGame.SDKEnabled)
        {
            YandexGame.savesData.gameProgress = progress;
            YandexGame.SaveProgress();
        }
    }


    public GameProgress LoadProgress()
    {
        GameProgress progress;

        if(YandexGame.SDKEnabled && YandexGame.savesData.gameProgress != null)
        {
            progress = YandexGame.savesData.gameProgress;
            Debug.Log("Progress loaded from YandexGames.savesData");
        } else
        {
            progress = LoadProgressLocal();
        }

        progress = ValidateProgress(progress);
        return progress;
    }

    public GameProgress LoadProgressLocal()
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

    private GameProgress ValidateProgress(GameProgress progress)
    {
        if (progress.Levels == null)
        {
            progress.Levels = new List<GameProgress.LevelData>();
        }

        for (int i = 1; i <= ExpectedLevelsCount; i++)
        {
            if (!progress.Levels.Any(l => l.LevelNumber == i))
            {
                progress.Levels.Add(new GameProgress.LevelData
                {
                    LevelNumber = i,
                    IsUnlocked = (i == 1), //only first level enabled by default
                    Score = 0
                });
            }
        }

        progress.Levels = progress.Levels.OrderBy(l => l.LevelNumber).ToList();
        return progress;
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
