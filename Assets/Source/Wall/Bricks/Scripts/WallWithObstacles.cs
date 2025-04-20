using System.Collections.Generic;
using UnityEngine;


public class WallWithObstacles : Wall
{
    [SerializeField] private ObstacleSettings _obstacleSettings;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private float _obstacleMinDistance = 5.0f;

    private List<Bounds> _occupiedAreas = new List<Bounds>();
    private List<GameObject> _obstacles = new List<GameObject>();

    private void OnDisable() => LevelController.Instance.OnLevelChanged -= OnLevelChanged;

    protected override void Start()
    {

        base.Start();
        _obstacles.Clear();
        _occupiedAreas = GetOccupiedAreas();
        InitObstacles();
        LevelController.Instance.OnLevelChanged += OnLevelChanged;
        SetLevel(LevelController.Instance.CurrentLevelInController);
    }

    public void StopObstacles()
    {
        foreach (GameObject obstacle in _obstacles)
        {
            
            if(obstacle != null)
                obstacle.SetActive(false);
        }
    }

    public void SetLevel(int level)
    {
        _currentLevel = level;
        ClearSceneObstacles();
        CheckClearObstaclesState();
        InitObstacles();
    }

    private void InitObstacles()
    {
        if (LevelController.Instance.IsRestartingLevel && PersistentData.SavedObstacles.Count > 0)
        {
            RestoreObstacles();
        }
        else
        {
            GenerateObstacles();
        }
    }

    private void RestoreObstacles()
    {
        _obstacles.Clear();
        foreach (ObstacleState state in PersistentData.SavedObstacles)
        {
            GameObject obstacle = Instantiate(state.Prefab, state.Position, state.Rotation);
            obstacle.transform.SetParent(transform);

            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = new Vector2(0, -state.Speed);

            Bounds bounds = new Bounds(state.Position, obstacle.transform.localScale * state.SizeScale);
            _occupiedAreas.Add(bounds);
            _obstacles.Add(obstacle);
        }
    }

    private void CheckClearObstaclesState()
    {
        if (!LevelController.Instance.IsRestartingLevel)
            PersistentData.SavedObstacles.Clear();

        _obstacles.Clear();
    }

    private void OnLevelChanged(int level)
    {
        SetLevel(level);
    }

    private void ClearSceneObstacles()
    {
        foreach(Transform child in transform)
        {
            if(child.GetComponent<IBrick>() == null)
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

        CheckClearObstaclesState();

        // Limit number of attempts to prevent infinite loops
        int maxAttempts = obstacleCount * 5;
        int attemptCount = 0;
        int placedObstacles = 0;

        while (placedObstacles < obstacleCount && attemptCount < maxAttempts)
        {
            attemptCount++;

            ObstacleData obstacleData = _obstacleSettings.GetRandomObstacleData();
            GameObject obstaclePrefab = obstacleData.ObstaclePrefab;
            if (obstaclePrefab == null) continue;

            // Calculate a random position within the wall bounds
            Vector3 randomPosition = new Vector3(
                Random.Range(LeftBound.x, RightBound.x),
                Random.Range(_topBound.y, _bottomBound.y),
                obstacleData.ZOffset);

            Bounds obstacleBounds = new Bounds(randomPosition, obstaclePrefab.transform.localScale * obstacleData.SizeScale);

            // Add some padding to ensure obstacles aren't too close
            Bounds paddedBounds = new Bounds(obstacleBounds.center, obstacleBounds.size + Vector3.one * _obstacleMinDistance);

            if (IsAreaValid(paddedBounds))
            {
                GameObject obstacle = Instantiate(obstaclePrefab, randomPosition, Quaternion.Euler(obstacleData.Rotation));
                obstacle.transform.SetParent(transform);

                float speed = Random.Range(minSpeed, maxSpeed);
                Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
                if (rb != null) rb.velocity = new Vector2(0, -speed);

                _occupiedAreas.Add(paddedBounds);
                SaveObstacle(obstacleData, obstaclePrefab, randomPosition, speed);

                _obstacles.Add(obstacle);
                placedObstacles++;
            }
        }

        // Log warning if we couldn't place all obstacles
        if (placedObstacles < obstacleCount)
        {
            Debug.LogWarning($"Could only place {placedObstacles} obstacles out of {obstacleCount} due to space constraints");
        }
    }

    private void SaveObstacle(ObstacleData obstacleData, GameObject obstaclePrefab, Vector3 randomPosition, float speed)
    {
        ObstacleState state = new ObstacleState
        {
            Prefab = obstaclePrefab,
            Position = randomPosition,
            Rotation = Quaternion.Euler(obstacleData.Rotation),
            Speed = speed,
            SizeScale = obstacleData.SizeScale
        };

        PersistentData.SavedObstacles.Add(state);
    }

    private List<Bounds> GetOccupiedAreas()
    {
        List<Bounds> occupiedAreas = new List<Bounds>();
        Transform[] children = GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if(renderer != null && child.GetComponent<IBrick>() == null)
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
