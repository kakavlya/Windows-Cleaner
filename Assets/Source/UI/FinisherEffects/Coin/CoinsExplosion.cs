using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void StartExplosion()
    {
        Vector3 startingPos = new Vector3(10f, 0f, -2f);
        for(int i =0 ; i < _coinsAmount; i++)
        {
            //GameObject.Instantiate(_3dCoin);
            // TODO add random rotation to coin
            // add random force to coin
            // add random location to coin accordint to starting position
            var randomPos = startingPos + Helpers.GetRandomXZPos(_spread);
            var randomRotation = Helpers.GetRandomRotation(_maxRandomRotationAngle);
            GameObject coin = GameObject.Instantiate(_3dCoin, randomPos, randomRotation);
            //coin.transform.position = transform.position;
            coin.SetActive(true);
            coin.GetComponent<Rigidbody>().AddForce(Vector3.up * _throwForce, ForceMode.Impulse);
            coin.GetComponent<CoinThrow>().SetSpinForce(_spinForce);
        }
    }
}
