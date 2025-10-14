using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace WindowsCleaner.GameProgressNs
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] private GameObject _logoPanel;
        [SerializeField] private float _minLoadingTime = 0.7f;

        private GameDataHandle _gameDataHandle;
        private float startTime;

        private void OnEnable()
        {
            YandexGame.GetDataEvent += OnYandexDataLoaded;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= OnYandexDataLoaded;
        }

        private void Start()
        {
            _gameDataHandle = new GameDataHandle();
            startTime = Time.time;
            if (_logoPanel != null)
                _logoPanel.SetActive(true);

            if (!YandexGame.SDKEnabled)
            {
                OnYandexDataLoaded();
            }
            else
            {
                if (YandexGame.savesData.gameProgress != null)
                {
                    OnYandexDataLoaded();
                }
            }
        }

        private IEnumerator ProceedToGame()
        {
            float elapsedTime = Time.time - startTime;
            if (elapsedTime < _minLoadingTime)
            {
                yield return new WaitForSeconds(_minLoadingTime - elapsedTime);
            }

            GameProgress loadedProgress = _gameDataHandle.LoadProgress();

            LoadHighestLevel();
        }
        private void OnYandexDataLoaded()
        {
            YandexGame.GetDataEvent -= OnYandexDataLoaded;
            StartCoroutine(ProceedToGame());
        }

        private void LoadHighestLevel()
        {
            int minimalLevelToLoad = 1;

            var progress = _gameDataHandle.LoadProgress();
            var levels = progress.Levels;
            int highestUnlockedLevel = progress.Levels
                .Where(level => level.IsUnlocked)
                .Select(level => level.LevelNumber)
                .DefaultIfEmpty(minimalLevelToLoad)
                .Max();

            LoadLevel(highestUnlockedLevel);
        }

        private void LoadLevel(int levelIndex)
        {
            LevelController.Instance.SetLevel(levelIndex);

            SceneManager.LoadScene("GameScene");
        }
    }
}