using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTypeSettings
{
    public List<ObstacleData> dataPrefabs;

    public AnimationCurve countCurve;   
    public int maxCount = 10;            

    public AnimationCurve speedMinCurve; 
    public AnimationCurve speedMaxCurve; 

    public float minSpeed = 5f;
    public float maxSpeed = 10f;

    
    public int GetCount(int level)
    {
        float normalized = countCurve.Evaluate(level);
        return Mathf.RoundToInt(normalized * maxCount);
    }

    public float GetMinSpeed(int level)
    {
        float normalized = speedMinCurve.Evaluate(level);
        return Mathf.Lerp(minSpeed, maxSpeed, normalized);
    }

    
    public float GetMaxSpeed(int level)
    {
        float normalized = speedMaxCurve.Evaluate(level);
        return Mathf.Lerp(minSpeed, maxSpeed, normalized);
    }

    
    public ObstacleData GetRandomData()
    {
        if (dataPrefabs == null || dataPrefabs.Count == 0)
            return null;
        return dataPrefabs[Random.Range(0, dataPrefabs.Count)];
    }
}

