using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistentData
{
    public static List<ObstacleState> SavedObstacles = new List<ObstacleState>();

    public static int? EnvironmentPrefabIndex;
}
