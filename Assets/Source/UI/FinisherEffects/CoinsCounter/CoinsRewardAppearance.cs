using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRewardAppearance : MonoBehaviour
{
    [SerializeField] private GameObject _coinsReward;
    //[SerializeField] private float _startSize = 0.1f;
    [SerializeField] private float _enlargeSize = 0.5f;
    [SerializeField] private float _duration = 1.2f;
    [SerializeField] private float delayBeforeDecrease = 0.5f;
    [SerializeField] private float _endSize = 2.2f;
    [SerializeField] private Ease _easingType = Ease.OutQuad;
    private Transform _coinsRewardTransform;

    public void StartRewardsSequence()
    {
        // should start from 0 coins and count to maximum, using duration variable
        // should pop up before coins will fly using dotween
        _coinsReward.SetActive(true);

        _coinsRewardTransform = _coinsReward.transform;
        _coinsRewardTransform.localScale = new Vector3(0, 0, 0);

        DisplayRewardsSequence(_coinsRewardTransform);
        TriggerRewardCount(_coinsReward.GetComponent<CoinsReward>());
    }

    private void TriggerRewardCount(CoinsReward coinsReward) => 
        coinsReward.TriggerRewardCount(_duration+1);

    private void DisplayRewardsSequence(Transform _coinsRewardTransform)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(_coinsRewardTransform.DOScale(_enlargeSize, _duration).SetEase(_easingType));

        sequence.AppendInterval(delayBeforeDecrease);

        sequence.Append(_coinsRewardTransform.DOScale(_endSize, _duration).SetEase(_easingType));
        
    }
}
