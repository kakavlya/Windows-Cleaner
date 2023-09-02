using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectedFinisher : MonoBehaviour
{
    [SerializeField] private float _waitingTimeCoroutineStarting = 1.5f;
    [SerializeField] private float _duration = 3.0f;
    [SerializeField] private float _ribbonDuration = 7.0f;

    private CoinsExplosion _coinsExplosion;
    private DustExplosion _dustExplosion;
    private UICoinsExplosion _uiCoinsExplosion;
    private ResultsRibbon _resultsRibbon;
    private CoinsRewardAppearance _coinsRewardAppearance;
    private void Start()
    {
        _coinsExplosion = GetComponent<CoinsExplosion>();
        _dustExplosion = GetComponent<DustExplosion>();
        _uiCoinsExplosion = GetComponent<UICoinsExplosion>();
        _resultsRibbon = GetComponent<ResultsRibbon>();
        _coinsRewardAppearance = GetComponent<CoinsRewardAppearance>();

        _coinsExplosion.SetDuration(_duration);
        _dustExplosion.SetDuration(_duration);
        _uiCoinsExplosion.SetDuration(_duration);
        _resultsRibbon.SetDuration(_ribbonDuration);
    }
    
    public void StartFinishingSequence()
    {
        // First sequence - start explosion of coins, coins flying up and dust object,
        // on sequence finishing - call the second sequence
        // sequence length 3 seconds
        // Second sequence - flying finishing ribbon, on sequence finished - call the buttons menu
        // sequence length 4 seconds




        // Coins should spawn in the middle with random x, y coordinate
        // A couple of seconds after spawn go to random location slightly upper
        // and than go to the upper right corner
        // Need 3 points -
        // starting point with a spread
        // middle point with less spread and end point
        StartCoroutine(AnimateFromPrefabRoutine());
    }

    private IEnumerator AnimateFromPrefabRoutine()
    {

        yield return new WaitForSeconds(_waitingTimeCoroutineStarting);

        _coinsExplosion.StartExplosion();
        _dustExplosion.PlayEffect();
        _uiCoinsExplosion.Start2DCoinsAnimation();
        _resultsRibbon.StartRibbonSequenceAfterDelay(_duration);
        _coinsRewardAppearance.StartRewardsSequence();
    }

}
