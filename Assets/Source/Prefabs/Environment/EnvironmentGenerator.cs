using System;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _environments;
    [SerializeField] private Vector3 _position;

    private void Start()
    {
        GenerateRandomEnvironment();
    }

    private void GenerateRandomEnvironment()
    {
        int envToGenerate = UnityEngine.Random.Range(0, _environments.Length);
        Instantiate(_environments[envToGenerate], _position, Quaternion.identity);
    }
}
