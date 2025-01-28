using System.Collections.Generic;
using UnityEngine;


public class WallWithObstacles : Wall
{
    [System.Serializable]
    public class ObstacleData
    {
        public GameObject ObstaclePrefab;
        public int Count;
        public Vector2 Size;
        public Vector3 Rotation;
        public float ZOffset;
    }



    [SerializeField] private List<ObstacleData> _obstacleTypes = new List<ObstacleData>();
    [SerializeField] private float _obstacleMinDistance = 1.0f;

    private List<Bounds> _occupiedAreas = new List<Bounds>();

    protected override void Start()
    {
        base.Start();
        _occupiedAreas = GetOccupiedAreas();
        GenerateObstacles();

    }

    private void GenerateObstacles()
    {
        foreach(var obstacleType in _obstacleTypes)
        {
            int generated = 0;
            while (generated < obstacleType.Count)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(LeftBound.x, RightBound.x),
                    Random.Range(_topBound.y, _bottomBound.y),
                    obstacleType.ZOffset);


                Bounds obstacleBounds = new Bounds(randomPosition, new Vector3(obstacleType.Size.x, obstacleType.Size.y));

                if (IsAreaValid(obstacleBounds))
                {
                    GameObject obstacle = Instantiate(obstacleType.ObstaclePrefab, randomPosition,
                        Quaternion.Euler(obstacleType.Rotation));
                    obstacle.transform.SetParent(transform);

                    //obstacle.transform.localScale = new Vector3(obstacleType.Size.x, obstacleType.Size.y, 1);

                    _occupiedAreas.Add(obstacleBounds);
                    generated++;
                }
            }
        }
    }

    private List<Bounds> GetOccupiedAreas()
    {
        List<Bounds> occupiedAreas = new List<Bounds>();
        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if(renderer != null && child.GetComponent<Brick>() == null)
            {
                occupiedAreas.Add(renderer.bounds);
            }
        }

        return occupiedAreas;
    }

    private bool IsAreaValid(Bounds newBounds)
    {
        foreach(var occupied in _occupiedAreas)
        {
            if(occupied.Intersects(newBounds)) return false;
        }

        return true;
    }


}
