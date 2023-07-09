using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectedFinisher : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _3DCoin;
    [SerializeField] private GameObject _Canvas;
    [SerializeField] private GameObject _StartPos;
    [SerializeField] private GameObject _spriteCoin;
    [SerializeField] private GameObject _UICoin;
    [SerializeField] private GameObject[] _UICoinsArr;
    [SerializeField] private float _minDuration = 1f;
    [SerializeField] private float _maxDuration = 3f;
    [SerializeField] private float _spread = 20f;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _maxCoins = 20;
    [SerializeField] private float _waitingTimeCoroutineStarting = 1.5f;
    private void Start()
    {
        //Animate();
        //AnimateUIFromPrefab();
        //AnimateUIExisting();
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
            Debug.Log("Temp coin pos: " + coin.transform.position);
            float randDuration = Random.Range(_minDuration, _maxDuration);
            coin.transform.DOLocalMove(targetPosition, randDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    coin.SetActive(false);
                });
        }
    }

    public void AnimateUIFromPrefab()
    {
        StartCoroutine(AnimateFromPrefabRoutine());
    }

    private IEnumerator AnimateFromPrefabRoutine()
    {

        yield return new WaitForSeconds(_waitingTimeCoroutineStarting);

        Vector3 startingPos = _startPos.position;
        Vector3 endPos = _endPos.position;

        for (int i = 0; i < _maxCoins; i++)
        {
            Vector3 coinPos = startingPos +
                new Vector3(Random.Range(-_spread, _spread), Random.Range(-_spread, _spread), 5f);

            GameObject coin = Instantiate(_UICoin, coinPos, Quaternion.identity, _Canvas.transform);

            coin.transform.position = startingPos
                + new Vector3(Random.Range(-_spread, _spread), Random.Range(-_spread, _spread), 0f);
            coin.transform.DOMove(endPos, 3f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    coin.SetActive(false);
                });
        }
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
