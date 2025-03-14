using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using YG;

public class Game : MonoBehaviour
{
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameObject _tutorialScreen;
    [SerializeField] private GameObject _touchControlsScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private EndLevelScreen _endLevelScreen;
    [SerializeField] private Player _player;
    [SerializeField] private CollectedFinisher _collectedFinisher;
    [SerializeField] private float _totalSequenceDuration;
    [SerializeField] private CinemachineVirtualCamera _secondCam;
    [SerializeField] private int _secondCamPriority;
    [SerializeField] private Scores _scores;  

    [SerializeField] private string _leaderBoardName = "WindowsCleanerLeaderboard";
    private LeaderboardService _leaderboardService;

    private void Awake()
    {
        _leaderboardService = new LeaderboardService(_leaderBoardName);
    }

    private void Start()
    {
        _startScreen.gameObject.SetActive(true);
        _touchControlsScreen.SetActive(false);
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
        _startScreen.gameObject.SetActive(false);
        _tutorialScreen?.SetActive(false);
        _touchControlsScreen.SetActive(true);
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
        _gameOverScreen.gameObject.SetActive(true);
        _touchControlsScreen?.SetActive(false);
    }
    private void WonLevel()
    {
        _touchControlsScreen.SetActive(false);
        float currentScore = _scores.GetCurrentScore();
        LevelController.Instance.CompleteLevel(currentScore);

        _collectedFinisher.StartFinishingSequence();

        _secondCam.Priority = _secondCamPriority;

        Invoke(nameof(ShowMenuAndPauseGame), _totalSequenceDuration);
    }

    private void ShowMenuAndPauseGame()
    {
        PauseGame();
        _endLevelScreen.gameObject.SetActive(true);
        _touchControlsScreen.SetActive(false);
        _leaderboardService.UpdateLeaderboard(LevelController.Instance.CurrentLevel);
    }

    private void UpdateLeaderBoard(int newRecord)
    {
        YandexGame.NewLeaderboardScores(_leaderBoardName, newRecord);
    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.gameObject.SetActive(false);
        LevelController.Instance.RestartingLevel();
        LevelController.Instance.ReloadScene();
    }

    private void OnMainMenuButtonClick()
    {
        Debug.Log("OnMainMenuButtonClick() clicked");
        SceneManager.LoadScene("StartingScene");
    }

    private void OnNextLevelButtonClick()
    {
        LevelController.Instance.NextLevel();
        LevelController.Instance.ReloadScene();
    }
}
