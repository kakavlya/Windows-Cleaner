using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectedFinisherOld : MonoBehaviour
{
    //[SerializeField] private Transform _startPos;
    //[SerializeField] private Transform _intermediatePos;
    //[SerializeField] private Transform _endPos;
    //[SerializeField] private Transform _target;
    //[SerializeField] private GameObject _3DCoin;
    //[SerializeField] private GameObject _Canvas;
    //[SerializeField] private GameObject _StartPos;
    //[SerializeField] private GameObject _spriteCoin;
    //[SerializeField] private GameObject _UICoin;
    //[SerializeField] private GameObject[] _UICoinsArr;
    //[SerializeField] private float _minDuration = 1f;
    //[SerializeField] private float _maxDuration = 2f;
    //[SerializeField] private float _spread = 30f;
    //[SerializeField] private Camera _camera;
    //[SerializeField] private int _maxCoins = 20;
    [SerializeField] private float _waitingTimeCoroutineStarting = 1.5f;
    [SerializeField] private float _duration = 3.0f;
    [SerializeField] private float _ribbonDuration = 7.0f;

    private CoinsExplosion _coinsExplosion;
    private DustExplosion _dustExplosion;
    private UICoinsExplosion _uiCoinsExplosion;
    private ResultsRibbon _resultsRibbon;
    private void Start()
    {
        _coinsExplosion = GetComponent<CoinsExplosion>();
        _dustExplosion = GetComponent<DustExplosion>();
        _uiCoinsExplosion = GetComponent<UICoinsExplosion>();
        _resultsRibbon = GetComponent<ResultsRibbon>();

        _coinsExplosion.SetDuration(_duration);
        _dustExplosion.SetDuration(_duration);
        _uiCoinsExplosion.SetDuration(_duration);
        _resultsRibbon.SetDuration(_ribbonDuration);
    }

    //private void AnimateSprites()
    //{
    //    Vector3 targetPosition = GetTargetPosition();
    //    for (int i = 0; i < _maxCoins; i++)
    //    {

    //        // Set position of the finishing object
    //        Vector3 coinPos = _StartPos.transform.position +
    //            new Vector3(Random.Range(-_spread, _spread), 0f, 5f);

    //        GameObject coin = GameObject.Instantiate(_spriteCoin, coinPos, Quaternion.identity);
    //        //Debug.Log("Temp coin pos: " + coin.transform.position);
    //        float randDuration = Random.Range(_minDuration, _maxDuration);
    //        coin.transform.DOLocalMove(targetPosition, randDuration)
    //            .SetEase(Ease.OutBack)
    //            .OnComplete(() =>
    //            {
    //                coin.SetActive(false);
    //            });
    //    }
    //}

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
    }

    

    //private void AnimateUIExisting()
    //{

    //    Vector3 startingPos = _startPos.position;
    //    Vector3 endPos = _endPos.position;

    //    foreach (var coin in _UICoinsArr)
    //    {
    //        coin.SetActive(true);

    //        coin.transform.position = startingPos
    //            + new Vector3(Random.Range(-_spread, _spread), 0, 0f);
    //        float randDuration = Random.Range(_minDuration, _maxDuration);
    //        coin.transform.DOMove(endPos, randDuration)
    //            .SetEase(Ease.OutBack);
    //    }
    //}

    //private void Animate3DObjects()
    //{
    //    // TODO Add something with 3d coins, they shouild rotate or something
    //    Vector3 targetPosition = GetTargetPosition();
    //    _3DCoin.transform.DOMove(targetPosition, _maxDuration);

    //    // 2D coins, try to use same position as for 3D, but use 2dSprite or 3dObject with texture

    //    Debug.Log("UICoin pos: " + _spriteCoin.transform.position);
    //    Debug.Log("_coin pos: " + _3DCoin.transform.position);
    //}

    //private Vector3 GetTargetPosition()
    //{
    //    //Debug.Log("Target transform: " + _target.position);
    //    var position = _target.position + new Vector3(0, 0, 5);
    //    Vector3 coolNewWorldPosition = _camera.ScreenToWorldPoint(position);
    //    Debug.Log("Position: " + coolNewWorldPosition);
    //    return coolNewWorldPosition;
    //}
}
