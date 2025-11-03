using UnityEngine;
using UnityEngine.Events;
using WindowsCleaner.PlayerNs;
using WindowsCleaner.WallNs;

namespace WindowsCleaner.Collectibles
{
    public class Scores : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Wall _wall;
        [SerializeField] private int _multiplier = 5;

        private int _totalBricksCount;
        private int _bricksHit;
        private float _currentScore;

        public event UnityAction<float> ProgressUpdated;

        private void Start()
        {
            _bricksHit = 0;
            _currentScore = 0f;
            GetTotalBricksCount();
        }

        private void OnEnable()
        {
            _player.IncrementScore += UpdateScore;
        }

        private void OnDisable()
        {
            _player.IncrementScore -= UpdateScore;
        }

        public int GetBricksHit()
        {
            return _bricksHit;
        }

        public float GetCurrentScore()
        {
            return _currentScore;
        }

        private void UpdateScore()
        {
            _bricksHit++;
            GetTotalBricksCount();
            _currentScore = (_bricksHit / (_totalBricksCount / 100f)) * _multiplier;
            ProgressUpdated?.Invoke(_currentScore);
        }

        private void GetTotalBricksCount()
        {
            _totalBricksCount = _wall.TotalBricks;
        }
    }
}