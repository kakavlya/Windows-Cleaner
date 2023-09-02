using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UICoinsExplosion : MonoBehaviour, IHaveDurartion
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _intermediatePos;
    [SerializeField] private Transform _endPos;
    [SerializeField] private GameObject _Canvas;
    [SerializeField] private GameObject _UICoin;
    [SerializeField] private float _minDuration = 1f;
    [SerializeField] private float _maxDuration = 2f;
    [SerializeField] private float _scaleSize = 0.5f;
    [SerializeField] private float _spread = 30f;
    [SerializeField] private int _maxCoins = 20;

    public void Start2DCoinsAnimation()
    {
        Vector3 startingPos = _startPos.position;
        Vector3 intermediatePos = _intermediatePos.position;
        Vector3 endPos = _endPos.position;

        for (int i = 0; i < _maxCoins; i++)
        {
            GameObject coin = Instantiate(_UICoin, _Canvas.transform);

            coin.transform.position = startingPos;
            Vector3 cointIntermedPos = intermediatePos + Helpers.GetRandomPosXY(_spread);
            coin.transform.DOMove(cointIntermedPos, _minDuration)
                .SetEase(Ease.Flash)
                .OnComplete(() =>
                {
                    float duration = Random.Range(_minDuration, _maxDuration);
                    coin.transform.DOScale(_scaleSize, duration);
                    coin.transform.DOMove(endPos, duration)
                    .SetEase(Ease.Flash)
                    .OnComplete(() =>
                    {
                        coin.SetActive(false);
                    });
                });
        }
    }

    public void SetDuration(float duration)
    {
        _maxDuration = duration;
    }
}
