using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameProgress;

public class LevelService 
{
    private readonly GameDataRepository _repository;

    public LevelService(GameDataRepository repository)
    {
        _repository = repository;
    }

    public int CompleteLevel(int currentLevelNumber, float newScore)
    {
        GameProgress progress = _repository.LoadProgress();

        LevelData currentLevel = progress.Levels.Find(level => level.LevelNumber == currentLevelNumber);
        if(currentLevel == null)
        {
            throw new System.Exception($"Level {currentLevelNumber} not found in progress data.");
        }

        if (newScore > currentLevel.Score)
        {
            currentLevel.Score = newScore;
        }

        progress.UpdateTotalScore();
        progress.CurrentLevel = currentLevelNumber;

        LevelData nextLevel = progress.Levels.Find(level => level.LevelNumber == currentLevelNumber+1);

        if (nextLevel != null && !nextLevel.IsUnlocked)
        {
            nextLevel.IsUnlocked = true;
        }

        _repository.SaveProgress(progress);
        return (int)progress.TotalScore;
    }
}
