using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WindowsCleaner.UI;

namespace WindowsCleaner.Obstacles
{
    [CreateAssetMenu(fileName = "ObstacleSettings", menuName = "Game/Obstacle Settings")]
    public class ObstacleSettings : ScriptableObject
    {
        [Header("Obstacle Variants")]
        [FormerlySerializedAs("obstacleDataPrefabs")]
        public List<ObstacleData> ObstacleDataPrefabs;

        [Header("Difficulty Scaling")]
        [FormerlySerializedAs("obstacleCountCurve")]
        public AnimationCurve ObstacleCountCurve;

        [FormerlySerializedAs("speedMinCurve")]
        public AnimationCurve SpeedMinCurve;

        [FormerlySerializedAs("speedMaxCurve")]
        public AnimationCurve SpeedMaxCurve;

        [Tooltip("Max number of obstacles on level")]
        [FormerlySerializedAs("MaxobstacleCount")]
        public int MaxObstacleCount = 10;

        [Tooltip("Minimal speed of obstacle")]
        [FormerlySerializedAs("MinSpeeLimit")]
        public float MinSpeedLimit = 5f;

        [Tooltip("Maximal speed of obstacle")]
        [FormerlySerializedAs("MaxSpeeLimit")]
        public float MaxSpeedLimit = 10f;

        public int GetObstacleCount(int level)
        {
            float normalized = ObstacleCountCurve.Evaluate(level);
            return Mathf.RoundToInt(normalized * MaxObstacleCount);
        }

        public float GetMinSpeed(int level)
        {
            float normalized = SpeedMinCurve.Evaluate(level);
            return Mathf.Lerp(MinSpeedLimit, MaxSpeedLimit, normalized);
        }

        public float GetMaxSpeed(int level)
        {
            float normalized = SpeedMaxCurve.Evaluate(level);
            return Mathf.Lerp(MinSpeedLimit, MaxSpeedLimit, normalized);
        }

        public ObstacleData GetRandomObstacleData()
        {
            if (ObstacleDataPrefabs == null || ObstacleDataPrefabs.Count == 0)
                return null;

            return ObstacleDataPrefabs[Random.Range(0, ObstacleDataPrefabs.Count)];
        }
    }
}