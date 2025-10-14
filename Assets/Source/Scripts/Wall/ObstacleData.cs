using UnityEngine;

namespace WindowsCleaner.UI
{
    [System.Serializable]
    public class ObstacleData
    {
        public GameObject ObstaclePrefab;
        public float SizeScale = 1.0f;
        public Vector3 Rotation;
        public float ZOffset;
    }
}