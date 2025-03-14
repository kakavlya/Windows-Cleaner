using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _environments;
    [SerializeField] private Vector3 _position;


    private void Start()
    {
        //Debug.Log($"PersistentData.CurrentEnvironment: {PersistentData.CurrentEnvironment}, level is restarting: {LevelController.Instance.IsRestartingLevel}");
        if (PersistentData.EnvironmentPrefabIndex.HasValue != null && LevelController.Instance.IsRestartingLevel)
        {
            int index = PersistentData.EnvironmentPrefabIndex.Value;
            GameObject env = Instantiate(_environments[index], _position, Quaternion.identity);
            env.transform.SetParent(transform);
        } else
        {
            GenerateRandomEnvironment();
        }
        
    }

    private void GenerateRandomEnvironment()
    {
        int envToGenerate = UnityEngine.Random.Range(0, _environments.Length);
        GameObject newEnv = Instantiate(_environments[envToGenerate], _position, Quaternion.identity);
        newEnv.transform.SetParent(transform);

        PersistentData.EnvironmentPrefabIndex = envToGenerate;
    }
}
