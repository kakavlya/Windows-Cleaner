using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
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

        if(YandexGame.SDKEnabled)
        {
            YandexGame.savesData.gameProgress = json;
            YandexGame.SaveProgress();
        }
    }


    public GameProgress LoadProgress()
    {
        GameProgress progress = null;

        if(YandexGame.SDKEnabled && !string.IsNullOrEmpty(YandexGame.savesData.gameProgress))
        {
            progress = JsonUtility.FromJson<GameProgress>(YandexGame.savesData.gameProgress);
            Debug.Log("Progress loaded from YandexGames.savesData");
        } else
        {
            progress = LoadProgressLocal();
        }

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
