using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ObstacleSettings", menuName ="Game/Obstacle Settings")]
public class ObstacleSettings : ScriptableObject
{
    [Header("Obstacle Variants")]
    public List<ObstacleData> obstacleDataPrefabs;

    [Header("Difficulty Scaling")]
    public AnimationCurve obstacleCountCurve;
    public AnimationCurve speedMinCurve;
    public AnimationCurve speedMaxCurve;

    [Tooltip("Max number of obstacles on level")]
    public int MaxobstacleCount = 10;

    [Tooltip("Minimal speed of obstacle")]
    public float MinSpeeLimit = 5f;

    [Tooltip("Maximal speed of obstacle")]
    public float MaxSpeeLimit = 10f;

    public int GetObstacleCount(int level)
    {
        float normalizedValue = obstacleCountCurve.Evaluate(level);
        return Mathf.RoundToInt(normalizedValue * MaxobstacleCount);
    }

    public float GetMinSpeed(int level)
    {
        float normalizedValue = speedMinCurve.Evaluate(level);
        return Mathf.Lerp(MinSpeeLimit, MaxSpeeLimit, normalizedValue);
    }

    public float GetMaxSpeed(int level)
    {
        float normalizedValue = speedMaxCurve.Evaluate(level);
        return Mathf.Lerp(MinSpeeLimit, MaxSpeeLimit, normalizedValue);
    }

    public ObstacleData GetRandomObstacleData()
    {
        if (obstacleDataPrefabs.Count == 0)
            return null;
        return obstacleDataPrefabs[Random.Range(0, obstacleDataPrefabs.Count)];
    }

}
