using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WindowsCleaner.WallNs;

namespace WindowsCleaner.Obstacles
{
    [CreateAssetMenu(fileName = "ObstacleSettings", menuName = "Game/Obstacle Settings")]
    public class ObstacleSettings : ScriptableObject
    {
        [Header("Obstacle Variants")]
        [FormerlySerializedAs("obstacleDataPrefabs")]
        [SerializeField] private List<ObstacleData> _obstacleDataPrefabs;

        [Header("Difficulty Scaling")]
        [FormerlySerializedAs("obstacleCountCurve")]
        [SerializeField] private AnimationCurve _obstacleCountCurve;

        [FormerlySerializedAs("speedMinCurve")]
        [SerializeField] private AnimationCurve _speedMinCurve;

        [FormerlySerializedAs("speedMaxCurve")]
        [SerializeField] private AnimationCurve _speedMaxCurve;

        [Tooltip("Max number of obstacles on level")]
        [FormerlySerializedAs("MaxobstacleCount")]
        [SerializeField] private int _maxObstacleCount = 10;

        [Tooltip("Minimal speed of obstacle")]
        [FormerlySerializedAs("MinSpeeLimit")]
        [SerializeField] private float _minSpeedLimit = 5f;

        [Tooltip("Maximal speed of obstacle")]
        [FormerlySerializedAs("MaxSpeeLimit")]
        [SerializeField] private float _maxSpeedLimit = 10f;

        public int GetObstacleCount(int level)
        {
            float normalized = _obstacleCountCurve.Evaluate(level);
            return Mathf.RoundToInt(normalized * _maxObstacleCount);
        }

        public float GetMinSpeed(int level)
        {
            float normalized = _speedMinCurve.Evaluate(level);
            return Mathf.Lerp(_minSpeedLimit, _maxSpeedLimit, normalized);
        }

        public float GetMaxSpeed(int level)
        {
            float normalized = _speedMaxCurve.Evaluate(level);
            return Mathf.Lerp(_minSpeedLimit, _maxSpeedLimit, normalized);
        }

        public ObstacleData GetRandomObstacleData()
        {
            if (_obstacleDataPrefabs == null || _obstacleDataPrefabs.Count == 0)
                return null;

            return _obstacleDataPrefabs[Random.Range(0, _obstacleDataPrefabs.Count)];
        }
    }
}