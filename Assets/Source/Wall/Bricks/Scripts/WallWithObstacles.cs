using System.Collections.Generic;
using UnityEngine;


public class WallWithObstacles : Wall
{
    [SerializeField] private ObstacleSettings _obstacleSettings;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private float _obstacleMinDistance = 5.0f;

    private List<Bounds> _occupiedAreas = new List<Bounds>();


    protected override void Start()
    {
        base.Start();
        _occupiedAreas = GetOccupiedAreas();
        GenerateObstacles();
    }

    public void SetLevel(int level)
    {
        _currentLevel = level;
        ClearObstacles();
        GenerateObstacles();
    }

    private void ClearObstacles()
    {
        foreach(Transform child in transform)
        {
            if(child.GetComponent<Brick>() == null)
            {
                Destroy(child.gameObject);
            }
        }
        _occupiedAreas.Clear();
    }

    private void GenerateObstacles()
    {

        int obstacleCount = _obstacleSettings.GetObstacleCount(_currentLevel);
        float minSpeed = _obstacleSettings.GetMinSpeed(_currentLevel);
        float maxSpeed = _obstacleSettings.GetMaxSpeed(_currentLevel);

        for (int i = 0; i < obstacleCount; i++)
        {
            ObstacleData obstacleData = _obstacleSettings.GetRandomObstacleData();
            GameObject obstaclePrefab = obstacleData.ObstaclePrefab;
            if (obstaclePrefab == null) continue;

            Vector3 randomPosition = new Vector3(
                Random.Range(LeftBound.x, RightBound.x),
                Random.Range(_topBound.y, _bottomBound.y),
                obstacleData.ZOffset);

            Bounds obstacleBounds = new Bounds(randomPosition, obstaclePrefab.transform.localScale);

            if (IsAreaValid(obstacleBounds))
            {
                GameObject obstacle = Instantiate(obstaclePrefab, randomPosition, Quaternion.Euler(obstacleData.Rotation));
                obstacle.transform.SetParent(transform);

                float speed = Random.Range(minSpeed, maxSpeed);
                Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = new Vector2(0, -speed);

                _occupiedAreas.Add(obstacleBounds);
            }
        }


        //foreach(var obstacleType in _obstacleTypes)
        //{
        //    int generated = 0;
        //    while (generated < obstacleType.Count)
        //    {
        //        Vector3 randomPosition = new Vector3(
        //            Random.Range(LeftBound.x, RightBound.x),
        //            Random.Range(_topBound.y, _bottomBound.y),
        //            obstacleType.ZOffset);


        //        Bounds obstacleBounds = new Bounds(randomPosition, new Vector3(obstacleType.Size.x, obstacleType.Size.y));

        //        if (IsAreaValid(obstacleBounds))
        //        {
        //            GameObject obstacle = Instantiate(obstacleType.ObstaclePrefab, randomPosition,
        //                Quaternion.Euler(obstacleType.Rotation));
        //            obstacle.transform.SetParent(transform);

        //            //obstacle.transform.localScale = new Vector3(obstacleType.Size.x, obstacleType.Size.y, 1);

        //            _occupiedAreas.Add(obstacleBounds);
        //            generated++;
        //        }
        //    }
        //}
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
