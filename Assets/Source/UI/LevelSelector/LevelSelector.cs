using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameProgress;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private string _gameProgressPrefs = "GameProgress";
    [SerializeField] private GameObject _levelButtonPrefab;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private int _totalLevels;

    private GameProgress _progress;
    private GameDataHandle _gameDataHandle;

    private bool[] _levelStatus;
    private void Start()
    {
        _gameDataHandle = new GameDataHandle();
        LoadLevels();
        EnsureAllLevelsExist(_progress, _totalLevels);
        LoadLevelStatus();
        GenerateLevelButton();
    }

    private void OnEnable()
    {
        _closeBtn.onClick.AddListener(CloseMenu);
    }

    private void OnDisable()
    {
        _closeBtn.onClick.RemoveListener(CloseMenu);
    }

    public void LoadLevels()
    {
        _progress = _gameDataHandle.LoadProgress();
        if(_progress.Levels.Count == 0)
        {
            _progress.Levels.Add(new LevelData { LevelNumber = 1, IsUnlocked = true, Score = 0 });
            for(int i = 2; i < _totalLevels; i++)
            {
                _progress.Levels.Add(new LevelData { LevelNumber = i, IsUnlocked = false, Score = 0 });
            }

            _gameDataHandle.SaveProgress(_progress);
        }
    }

    private void LoadLevelStatus()
    {
        _levelStatus = new bool[_totalLevels];
        for (int i = 0; i < _totalLevels; i++)
        {
            _levelStatus[i] = _progress.Levels[i].IsUnlocked;
        }
    }

    private void GenerateLevelButton()
    {
        for (int i =0; i < _totalLevels; i++)
        {
            GameObject button = Instantiate(_levelButtonPrefab, _contentParent);
            button.GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();

            if (_levelStatus[i])
            {
                button.GetComponent<Button>().interactable = true;
                int levelIndex = i;
                button.GetComponent<Button>().onClick.AddListener(() => LoadNextLevel(levelIndex));
            } else
            {
                button.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        LevelController.Instance.SetLevel(levelIndex);
        LevelController.Instance.GoingNextLevel();
        SceneManager.LoadScene("GameScene");
    }

    public void LoadNextLevel(int levelIndex)
    {
        int selectedLevel = levelIndex + 1;
        LoadLevel(selectedLevel);
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void EnsureAllLevelsExist(GameProgress progress, int totalLevelsInGame)
    {
        int currentLevelCount = progress.Levels.Count;
        for (int i = currentLevelCount + 1; i <= totalLevelsInGame; i++)
        {
            progress.Levels.Add(new LevelData { LevelNumber = i, IsUnlocked = false, Score = 0 });
        }
    }
}
