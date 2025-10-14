using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using YG;
using WindowsCleaner.UI;
using WindowsCleaner.Collectibles;
using WindowsCleaner.GameProgressNs;
using WindowsCleaner.Obstacles;
using WindowsCleaner.WallNs;

namespace WindowsCleaner.Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private GameObject _tutorialScreen;
        [SerializeField] private GameObject _touchControlsScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private EndLevelScreen _endLevelScreen;
        [SerializeField] private GameObject _sliderPickedBar;
        [SerializeField] private PlayerNs.Player _player;
        [SerializeField] private CollectedFinisher _collectedFinisher;
        [SerializeField] private float _totalSequenceDuration;
        [SerializeField] private CinemachineVirtualCamera _secondCam;
        [SerializeField] private int _secondCamPriority;
        [SerializeField] private Scores _scores;
        [SerializeField] private WallWithObstacles _wall;
        [SerializeField] private UIStateMachine _uiStateMachine;

        [SerializeField] private string _leaderBoardName = "WindowsCleanerLeaderboard";
        private LeaderboardService _leaderboardService;

        private void Awake()
        {
            _leaderboardService = new LeaderboardService(_leaderBoardName);
        }

        private void Start()
        {
            _uiStateMachine.SwitchState(UIState.StartScreen);
            PauseGame();
        }

        private void OnEnable()
        {
            _startScreen.StartButtonClick += OnPlayButtonClick;
            _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
            _gameOverScreen.MainMenuButtonClick += OnMainMenuButtonClick;
            _endLevelScreen.RestartButtonClick += OnRestartButtonClick;
            _endLevelScreen.MainMenuButtonClick += OnMainMenuButtonClick;
            _endLevelScreen.NextButtonClick += OnNextLevelButtonClick;
            _player.GameOver += GameOver;
            _player.WonLevel += WonLevel;
        }

        private void OnDisable()
        {
            _startScreen.StartButtonClick -= OnPlayButtonClick;
            _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
            _gameOverScreen.MainMenuButtonClick -= OnMainMenuButtonClick;
            _endLevelScreen.RestartButtonClick -= OnRestartButtonClick;
            _endLevelScreen.MainMenuButtonClick -= OnMainMenuButtonClick;
            _endLevelScreen.NextButtonClick -= OnNextLevelButtonClick;
            _player.GameOver -= GameOver;
            _player.WonLevel -= WonLevel;
        }

        private void OnPlayButtonClick()
        {
            _uiStateMachine.SwitchState(UIState.Playing);
            StartGame();
        }

        private void StartGame()
        {
            Time.timeScale = 1;
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
        }

        private void GameOver()
        {
            PauseGame();
            _uiStateMachine.SwitchState(UIState.GameOver);
        }
        private void WonLevel()
        {
            _wall.StopObstacles();
            //_touchControlsScreen.SetActive(false);
            float currentScore = _scores.GetCurrentScore();
            LevelController.Instance.CompleteLevel(currentScore);
            _uiStateMachine.SwitchState(UIState.EndLevelAnimation);
            _collectedFinisher.StartFinishingSequence();

            _secondCam.Priority = _secondCamPriority;

            Invoke(nameof(ShowMenuAndPauseGame), _totalSequenceDuration);
        }

        private void ShowMenuAndPauseGame()
        {
            PauseGame();
            _uiStateMachine.SwitchState(UIState.EndLevel);
            _leaderboardService.UpdateLeaderboard(LevelController.Instance.CurrentLevelInController);
        }

        private void UpdateLeaderBoard(int newRecord)
        {
            YandexGame.NewLeaderboardScores(_leaderBoardName, newRecord);
        }

        private void OnRestartButtonClick()
        {
            LevelController.Instance.RestartingLevel();
            LevelController.Instance.ReloadScene();
        }

        private void OnMainMenuButtonClick()
        {
            SceneManager.LoadScene("StartingScene");
        }

        private void OnNextLevelButtonClick()
        {
            PersistentData.SavedObstacles.Clear();
            PersistentData.EnvironmentPrefabIndex = null;
            LevelController.Instance.NextLevel();
            LevelController.Instance.ReloadScene();
        }
    }
}