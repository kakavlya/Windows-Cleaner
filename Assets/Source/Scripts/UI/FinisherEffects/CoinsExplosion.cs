using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsCleaner.Core;

namespace WindowsCleaner.UI
{
    public class CoinsExplosion : MonoBehaviour
    {
        [SerializeField] private GameObject _3dCoin;
        [SerializeField] private int _coinsAmount = 10;
        [SerializeField] private GameObject _explosionCenter;
        [SerializeField] private float _throwForce = 5f;
        [SerializeField] private float _spinForce = 1f;
        [SerializeField] private float _spread = 1f;
        [SerializeField] private float _maxRandomRotationAngle = 360f;
        [SerializeField] private float _minMagnitude = 0.1f;
        [SerializeField] private float _maxMagnitude = 1f;
        [SerializeField] private Vector3 _startingPos = new (10f, 0f, -2f);

        [SerializeField] private float _ñoinsOnScreenDuration = 0.5f;

        public void StartExplosion()
        {
            for (int i = 0; i < _coinsAmount; i++)
            {
                var randomPos = _startingPos + Helpers.GetRandomXZPos(_spread);
                var randomRotation = Helpers.GetRandomRotation(_maxRandomRotationAngle);
                GameObject coin = Instantiate(_3dCoin, randomPos, randomRotation);
                coin.SetActive(true);
                coin.GetComponent<Rigidbody>().AddForce(Vector3.up * _throwForce, ForceMode.Impulse);
                coin.GetComponent<CoinThrow>().SetSpinForce(_spinForce);
                coin.GetComponent<CoinSizeDecrease>()?.StartDecreaseSequence(_ñoinsOnScreenDuration);
            }
        }

        public void SetDuration(float duration)
        {
            _ñoinsOnScreenDuration = duration;
        }
    }
}