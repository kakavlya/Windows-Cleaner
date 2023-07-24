using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DustExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _dustObject;
    [SerializeField] private int _amountOfDusts = 10;
    [SerializeField] private float _spread = 5f;
    [SerializeField] private float _maxRandomRotationAngle = 360f;

    private void Start()
    {
        CreateDustObjects();
    }

    private void CreateDustObjects()
    {
        for(int i =0;  i < _amountOfDusts; i++)
        {
            var randomPos = gameObject.transform.position + GetRandomPos();
            GameObject dust = GameObject.Instantiate(_dustObject, randomPos, GetRandomRotation());
        }
    }

    private Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(-_spread, _spread), Random.Range(-_spread, _spread), Random.Range(-_spread, _spread));
    }

    private Quaternion GetRandomRotation()
    {
        // Generate a random Euler rotation
        Vector3 randomEulerRotation = new Vector3(Random.Range(0f, _maxRandomRotationAngle),
                                                  Random.Range(0f, _maxRandomRotationAngle),
                                                  Random.Range(0f, _maxRandomRotationAngle));

        // Convert the Euler rotation to a Quaternion
        return Quaternion.Euler(randomEulerRotation);
    }
}
