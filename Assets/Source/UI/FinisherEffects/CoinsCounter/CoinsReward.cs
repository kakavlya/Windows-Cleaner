using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinsReward : MonoBehaviour
{
    [SerializeField] private TMP_Text _hitScore;
    [SerializeField] private Scores _scores;
    [SerializeField] private float _updatesDuringSecond = 100f;

    private int _totalRewardCount;
    private float _currentDisplayedReward;
    private float _stepsToMake;
    private float _stepsLeft;
    private float _scoresInStep;


    public void ShowCoinsPrize() =>
        ShowCoinsPrize(_totalRewardCount);

    public void ShowCoinsPrize(int score) =>
        _hitScore.SetText(score.ToString());

    private void GetCoinsPrize(int scores)
    {
        // TODO implement logic for reward calculation;
        _totalRewardCount = scores * 10;
    }

    private void CalculateShowReward(int scores)
    {
        GetCoinsPrize(scores);
    }

    private void OnEnable()
    {
        Debug.Log("CoinsReward: script was enabled");
        CalculateShowReward(_scores.GetBricksHit());
        ShowCoinsPrize(0);
    }

    internal void TriggerRewardCount(float _durationSeconds)
    {
        CalculateUpdateValues(_durationSeconds);
    }

    private void CalculateUpdateValues(float duration)
    {
        CalculateShowReward(_scores.GetBricksHit());


        float milliSecondsToWait = duration / _updatesDuringSecond;
        _stepsToMake = duration * _updatesDuringSecond;
        _stepsLeft = _stepsToMake;
        _scoresInStep = _totalRewardCount / _stepsToMake;
        //Debug.Log(String.Format("_steps to make {0}, _stepsLeft: {1}, _scoresInStep{2}, milliSecondsToWait{3}",
        //    _stepsToMake, _stepsLeft, _scoresInStep, milliSecondsToWait));
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
            //Debug.Log(String.Format(" _stepsLeft: {0}, _currentDisplayedReward{1}", _stepsLeft, _currentDisplayedReward));
            int rounded = Convert.ToInt32(_currentDisplayedReward);
            ShowCoinsPrize(rounded);
            StartCoroutine(StartUpdatingScores(milliSecondsToWait));
        }
    }
}
