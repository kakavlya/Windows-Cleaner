using System.Collections.Generic;

namespace WindowsCleaner.Obstacles
{

    public static class PersistentData
    {
        public static List<ObstacleState> SavedObstacles = new List<ObstacleState>();

        public static int? EnvironmentPrefabIndex;
    }
}