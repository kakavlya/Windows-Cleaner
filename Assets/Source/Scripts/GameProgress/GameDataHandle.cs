using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

namespace WindowsCleaner.GameProgressNs
{
    public class GameDataHandle
    {
        private const string GameProgressPref = "GameProgress";
        private readonly int _expectedLevelsCount = 50;

        public void SaveProgress(GameProgress progress)
        {
            string json = JsonUtility.ToJson(progress);
            PlayerPrefs.SetString(GameProgressPref, json);
            PlayerPrefs.Save();

            if (YandexGame.SDKEnabled)
            {
                YandexGame.savesData.gameProgress = progress;
                YandexGame.SaveProgress();
            }
        }

        public GameProgress LoadProgress()
        {
            GameProgress progress;

            if (YandexGame.SDKEnabled && YandexGame.savesData.gameProgress != null)
            {
                progress = YandexGame.savesData.gameProgress;
            }
            else
            {
                progress = LoadProgressLocal();
            }

            progress = ValidateProgress(progress);
            return progress;
        }

        public GameProgress LoadProgressLocal()
        {
            if (PlayerPrefs.HasKey(GameProgressPref))
            {
                string json = PlayerPrefs.GetString(GameProgressPref);
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
                progress.Levels = new List<LevelData>();
            }

            for (int i = 1; i <= _expectedLevelsCount; i++)
            {
                if (!progress.Levels.Any(l => l.LevelNumber == i))
                {
                    progress.Levels.Add(new LevelData
                    {
                        LevelNumber = i,
                        IsUnlocked = i == 1,
                        Score = 0,
                    });
                }
            }

            progress.Levels = progress.Levels.OrderBy(l => l.LevelNumber).ToList();
            return progress;
        }

        private void DeleteGameProgress()
        {
            PlayerPrefs.DeleteKey(GameProgressPref);
            PlayerPrefs.Save();
        }
    }
}