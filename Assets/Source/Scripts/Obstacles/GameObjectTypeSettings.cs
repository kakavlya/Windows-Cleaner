using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WindowsCleaner.WallNs;

namespace WindowsCleaner.Obstacles
{
    [System.Serializable]
    public class GameObjectTypeSettings
    {
        [FormerlySerializedAs("dataPrefabs")]
        [SerializeField] private List<ObstacleData> _dataPrefabs;

        [FormerlySerializedAs("countCurve")]
        [SerializeField] private AnimationCurve _countCurve;

        [FormerlySerializedAs("maxCount")]
        [SerializeField] private int _maxCount = 10;

        [FormerlySerializedAs("speedMinCurve")]
        [SerializeField] private AnimationCurve _speedMinCurve;

        [FormerlySerializedAs("speedMaxCurve")]
        [SerializeField] private AnimationCurve _speedMaxCurve;

        [FormerlySerializedAs("minSpeed")]
        [SerializeField] private float _minSpeed = 5f;

        [FormerlySerializedAs("maxSpeed")]
        [SerializeField] private float _maxSpeed = 10f;

        public int GetCount(int level)
        {
            float normalized = _countCurve.Evaluate(level);
            return Mathf.RoundToInt(normalized * _maxCount);
        }

        public float GetMinSpeed(int level)
        {
            float normalized = _speedMinCurve.Evaluate(level);
            return Mathf.Lerp(_minSpeed, _maxSpeed, normalized);
        }

        public float GetMaxSpeed(int level)
        {
            float normalized = _speedMaxCurve.Evaluate(level);
            return Mathf.Lerp(_minSpeed, _maxSpeed, normalized);
        }

        public ObstacleData GetRandomData()
        {
            if (_dataPrefabs == null || _dataPrefabs.Count == 0)
                return null;

            return _dataPrefabs[Random.Range(0, _dataPrefabs.Count)];
        }
    }
}