using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectedFinisher : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _intermediatePos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _3DCoin;
    [SerializeField] private GameObject _Canvas;
    [SerializeField] private GameObject _StartPos;
    [SerializeField] private GameObject _spriteCoin;
    [SerializeField] private GameObject _UICoin;
    [SerializeField] private GameObject[] _UICoinsArr;
    [SerializeField] private float _minDuration = 1f;
    [SerializeField] private float _maxDuration = 2f;
    [SerializeField] private float _durationToIntermediate = 2f;
    [SerializeField] private float _durationToFinale = 1.5f;
    [SerializeField] private float _spread = 30f;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _maxCoins = 20;
    [SerializeField] private float _waitingTimeCoroutineStarting = 1.5f;

    private CoinsExplosion _coinsExplosion;
    private void Start()
    {
        //Animate();
        //AnimateUIFromPrefab();
        //AnimateUIExisting();
        _coinsExplosion = GetComponent<CoinsExplosion>();
    }

    private void AnimateSprites()
    {
        Vector3 targetPosition = GetTargetPosition();
        for (int i = 0; i < _maxCoins; i++)
        {

            // Set position of the finishing object
            Vector3 coinPos = _StartPos.transform.position +
                new Vector3(Random.Range(-_spread, _spread), 0f, 5f);

            GameObject coin = GameObject.Instantiate(_spriteCoin, coinPos, Quaternion.identity);
            //Debug.Log("Temp coin pos: " + coin.transform.position);
            float randDuration = Random.Range(_minDuration, _maxDuration);
            coin.transform.DOLocalMove(targetPosition, randDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    coin.SetActive(false);
                });
        }
    }

    public void StartFinishingSequence()
    {
        // TODO Make explosion with 3d coins and grey cubes as a smoke

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

        Start2DCoinsAnimation();
    }

    private void Start2DCoinsAnimation()
    {
        Vector3 startingPos = _startPos.position;
        Vector3 intermediatePos = _intermediatePos.position;
        Vector3 endPos = _endPos.position;

        for (int i = 0; i < _maxCoins; i++)
        {
            GameObject coin = Instantiate(_UICoin, _Canvas.transform);

            coin.transform.position = startingPos;
            Vector3 cointIntermedPos = intermediatePos + GetRandomPos();
            coin.transform.DOMove(cointIntermedPos, _minDuration)
                .SetEase(Ease.Flash)
                .OnComplete(() =>
                {
                    coin.transform.DOMove(endPos, Random.Range(_minDuration, _maxDuration))
                    .SetEase(Ease.Flash)
                    .OnComplete(() =>
                    {
                        coin.SetActive(false);
                    });
                });
        }
    }

    private Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(-_spread, _spread), Random.Range(-_spread, _spread), 0f);
    }

    private void AnimateUIExisting()
    {

        Vector3 startingPos = _startPos.position;
        Vector3 endPos = _endPos.position;

        foreach (var coin in _UICoinsArr)
        {
            coin.SetActive(true);

            coin.transform.position = startingPos
                + new Vector3(Random.Range(-_spread, _spread), 0, 0f);
            float randDuration = Random.Range(_minDuration, _maxDuration);
            coin.transform.DOMove(endPos, randDuration)
                .SetEase(Ease.OutBack);
        }
    }

    private void Animate3DObjects()
    {
        // TODO Add something with 3d coins, they shouild rotate or something
        Vector3 targetPosition = GetTargetPosition();
        _3DCoin.transform.DOMove(targetPosition, _maxDuration);

        // 2D coins, try to use same position as for 3D, but use 2dSprite or 3dObject with texture

        Debug.Log("UICoin pos: " + _spriteCoin.transform.position);
        Debug.Log("_coin pos: " + _3DCoin.transform.position);
    }

    private Vector3 GetTargetPosition()
    {
        //Debug.Log("Target transform: " + _target.position);
        var position = _target.position + new Vector3(0, 0, 5);
        Vector3 coolNewWorldPosition = _camera.ScreenToWorldPoint(position);
        Debug.Log("Position: " + coolNewWorldPosition);
        return coolNewWorldPosition;
    }
}
