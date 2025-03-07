using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private LevelSelector _levelSelector;
    private GameDataHandle _gameDataHandle;

    private void Start()
    {
        _startButton.onClick.AddListener(LoadLevel);
        _gameDataHandle = new GameDataHandle();
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadLevel);
    }

    private void LoadLevel()
    {

        // Temp Solution to reset progress
        //_gameDataHandle.ResetGameProgress();
        
        var progress = _gameDataHandle.LoadProgress();
        var levels = progress.Levels;
        int levelToLoad = 1;
        for(int i = 0; i< levels.Count; i++)
        {
            if (!levels[i].IsUnlocked)
            {
                
                levelToLoad = Mathf.Max(levelToLoad, levels[i].LevelNumber - 1);
                break;
            }
            levelToLoad = i;
        }

        Debug.Log("Levels " + levels.Count);
        Debug.Log("First Locked level " + levelToLoad);


        //SceneManager.LoadScene("Level" + (levelToLoad));
        _levelSelector.LoadLevel(levelToLoad);
    }
}
