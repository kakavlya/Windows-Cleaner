using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataRepository 
{
    private const string _gameProgressPref = "GameProgress";
    private readonly GameDataHandle _gameDataHandle = new GameDataHandle();

    public GameProgress LoadProgress()
    {
        return _gameDataHandle.LoadProgress();
    }

    public void SaveProgress(GameProgress progress)
    {
        _gameDataHandle.SaveProgress(progress);
    }
}
