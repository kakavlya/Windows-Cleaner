using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameProgress;

public class Level : MonoBehaviour
{

    public void CompleteLevel(int currentLevelNumber, float newScore)
    {
        GameProgress progress = LoadProgress();

        // Ќаходим текущий уровень по номеру
        LevelData currentLevel = progress.Levels.Find(level => level.LevelNumber == currentLevelNumber);

        if (currentLevel != null)
        {
            // ќбновл€ем счет уровн€, если новый счет выше
            if (newScore > currentLevel.Score)
            {
                currentLevel.Score = newScore;
                progress.UpdateTotalScore(); // ѕересчитываем общий счет
            }

            // ќткрываем следующий уровень, если он существует и еще не открыт
            LevelData nextLevel = progress.Levels.Find(level => level.LevelNumber == currentLevelNumber + 1);
            if (nextLevel != null && !nextLevel.IsUnlocked)
            {
                nextLevel.IsUnlocked = true;
            }

            // —охран€ем обновленный прогресс
            SaveProgress(progress);
        }
    }

    public void SaveProgress(GameProgress progress)
    {
        string json = JsonUtility.ToJson(progress);
        PlayerPrefs.SetString("GameProgress", json);
        PlayerPrefs.Save();
    }

    public GameProgress LoadProgress()
    {
        if (PlayerPrefs.HasKey("GameProgress"))
        {
            string json = PlayerPrefs.GetString("GameProgress");
            return JsonUtility.FromJson<GameProgress>(json);
        }
        else
        {
            GameProgress newProgress = new GameProgress();
            newProgress.Levels.Add(new LevelData { LevelNumber = 1, IsUnlocked = true, Score = 0 });
            for (int i = 2; i <= 10; i++)
            {
                newProgress.Levels.Add(new LevelData { LevelNumber = i, IsUnlocked = false, Score = 0 });
            }
            SaveProgress(newProgress);
            return newProgress;
        }
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
    }
}
