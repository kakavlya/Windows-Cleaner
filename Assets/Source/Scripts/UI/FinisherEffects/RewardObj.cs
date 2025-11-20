using System;
using System.Collections;
using TMPro;
using UnityEngine;
using WindowsCleaner.Collectibles;

namespace WindowsCleaner.UI
{
    public class RewardObj : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hitScore;
        [SerializeField] private Scores _scores;
        [SerializeField] private float _updatesDuringSecond = 100f;

        private int _totalRewardCount;
        private float _currentDisplayedReward;
        private float _stepsToMake;
        private float _stepsLeft;
        private float _scoresInStep;

        private void OnEnable()
        {
            CalculateShowReward(_scores.GetBricksHit());
            ShowCoinsPrize(0);
        }

        public void ShowCoinsPrize()
        {
            ShowCoinsPrize(_totalRewardCount);
        }

        public void ShowCoinsPrize(int score)
        {
            _hitScore.SetText(score.ToString());
        }

        public void TriggerRewardCount(float durationSeconds)
        {
            CalculateUpdateValues(durationSeconds);
        }

        private void GetCoinsPrize(int scores)
        {
            _totalRewardCount = scores * 10;
        }

        private void CalculateShowReward(int scores)
        {
            GetCoinsPrize(scores);
        }

        private void CalculateUpdateValues(float duration)
        {
            CalculateShowReward(_scores.GetBricksHit());

            float milliSecondsToWait = duration / _updatesDuringSecond;
            _stepsToMake = duration * _updatesDuringSecond;
            _stepsLeft = _stepsToMake;
            _scoresInStep = _totalRewardCount / _stepsToMake;
            IEnumerator coroutine = StartUpdatingScores(milliSecondsToWait);
            StartCoroutine(coroutine);
        }

        private IEnumerator StartUpdatingScores(float milliSecondsToWait)
        {
            yield return new WaitForSeconds(milliSecondsToWait);

            if (_stepsLeft > 0)
            {
                _stepsLeft--;
                _currentDisplayedReward += _scoresInStep;
                int rounded = Convert.ToInt32(_currentDisplayedReward);
                ShowCoinsPrize(rounded);
                StartCoroutine(StartUpdatingScores(milliSecondsToWait));
            }
        }
    }
}