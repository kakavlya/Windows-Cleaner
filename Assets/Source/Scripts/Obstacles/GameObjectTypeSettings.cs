using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WindowsCleaner.UI;

namespace WindowsCleaner.Obstacles
{
    [System.Serializable]
    public class GameObjectTypeSettings
    {
        [FormerlySerializedAs("dataPrefabs")]
        public List<ObstacleData> DataPrefabs;

        [FormerlySerializedAs("countCurve")]
        public AnimationCurve CountCurve;

        [FormerlySerializedAs("maxCount")]
        public int MaxCount = 10;

        [FormerlySerializedAs("speedMinCurve")]
        public AnimationCurve SpeedMinCurve;

        [FormerlySerializedAs("speedMaxCurve")]
        public AnimationCurve SpeedMaxCurve;

        [FormerlySerializedAs("minSpeed")]
        public float MinSpeed = 5f;

        [FormerlySerializedAs("maxSpeed")]
        public float MaxSpeed = 10f;

        public int GetCount(int level)
        {
            float normalized = CountCurve.Evaluate(level);
            return Mathf.RoundToInt(normalized * MaxCount);
        }

        public float GetMinSpeed(int level)
        {
            float normalized = SpeedMinCurve.Evaluate(level);
            return Mathf.Lerp(MinSpeed, MaxSpeed, normalized);
        }

        public float GetMaxSpeed(int level)
        {
            float normalized = SpeedMaxCurve.Evaluate(level);
            return Mathf.Lerp(MinSpeed, MaxSpeed, normalized);
        }

        public ObstacleData GetRandomData()
        {
            if (DataPrefabs == null || DataPrefabs.Count == 0)
                return null;

            return DataPrefabs[Random.Range(0, DataPrefabs.Count)];
        }
    }
}