//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using YG;
//using static GameProgress;

//public class Level : MonoBehaviour
//{
//    [SerializeField] private string _leaderBoardName = "WindowsCleanerLeaderboard";

//    private LevelService _levelService;
//    private GameDataRepository _repository;

//    private void Awake()
//    {
//        _repository = new GameDataRepository();
//        _levelService = new LevelService(_repository);
//    }

//    public void OnLevelCompleted(int levelNumber, float score)
//    {
//        int newTotalScore = _levelService.CompleteLevel(levelNumber, score);
//        UpdateLeaderBoard(newTotalScore);

//        LevelController.Instance.NextLevel();
//        LevelController.Instance.ReloadCurrentScene();
//    }

//    public void OnLevelCompleted(int levelNumber)
//    {
//        LevelController.Instance.NextLevel();
//        LevelController.Instance.ReloadCurrentScene();
//    }

//    private void UpdateLeaderBoard(int newRecord)
//    {
//        YandexGame.NewLeaderboardScores(_leaderBoardName, newRecord);
//    }
//}
