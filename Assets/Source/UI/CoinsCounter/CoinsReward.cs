using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsReward : MonoBehaviour
{
    [SerializeField] private TMP_Text _hitScore;
    private int _rewardCount;
    [SerializeField] private Scores _scores;

    public void SetCoinsPrize()
    {
        _hitScore.SetText(_rewardCount.ToString());
    }

    private void GetCoinsPrize(int scores)
    {
        // TODO implement logic for reward calculation;
        _rewardCount = scores * 10;
    }

    private void CalculateShowReward(int scores)
    {
        GetCoinsPrize(scores);
        SetCoinsPrize();
    }

    private void OnEnable()
    {
        Debug.Log("CoinsReward: script was enabled");
        CalculateShowReward(_scores.GetBricksHit());
    }
}
