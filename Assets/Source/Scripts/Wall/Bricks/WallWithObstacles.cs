using System.Collections.Generic;
using UnityEngine;

public class WallWithObstacles : Wall
{
    [Header("Config")]
    [SerializeField] private ObstacleSettings _obstacleSettings;
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private float _obstacleMinDistance = 5.0f;

    private readonly List<Bounds> _occupiedAreas = new List<Bounds>();
    private readonly List<GameObject> _obstacles = new List<GameObject>();

    private void OnEnable()
    {
        if (LevelController.Instance != null)
            LevelController.Instance.OnLevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        if (LevelController.Instance != null)
            LevelController.Instance.OnLevelChanged -= OnLevelChanged;
    }

    protected override void Start()
    {
        base.Start();

        _obstacles.Clear();
        RebuildOccupiedAreas();

        int startLevel = (LevelController.Instance != null)
            ? LevelController.Instance.CurrentLevelInController
            : _currentLevel;

        SetLevel(startLevel, forceRebuild: true);
    }

    public void StopObstacles()
    {
        foreach (var obstacle in _obstacles)
        {
            if (obstacle != null)
                obstacle.SetActive(false);
        }
    }

    public void SetLevel(int level, bool forceRebuild = false)
    {
        if (!forceRebuild && level == _currentLevel)
            return;

        _currentLevel = level;
        ClearSceneObstacles();
        ClearSavedStateIfNeeded();
        InitObstacles();
    }

    private void OnLevelChanged(int level)
    {
        SetLevel(level, forceRebuild: true);
    }

    private void InitObstacles()
    {
        if (LevelController.Instance != null &&
            LevelController.Instance.IsRestartingLevel &&
            PersistentData.SavedObstacles.Count > 0)
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

        foreach (var state in PersistentData.SavedObstacles)
        {
            var obstacle = Instantiate(state.Prefab, state.Position, state.Rotation);
            obstacle.transform.SetParent(transform);

            var rb2d = obstacle.GetComponent<Rigidbody2D>();
            if (rb2d != null)
                rb2d.velocity = new Vector2(0f, -state.Speed);

            var renderer = obstacle.GetComponentInChildren<Renderer>();
            var size = (renderer != null) ? renderer.bounds.size : obstacle.transform.localScale * state.SizeScale;

            var bounds = new Bounds(state.Position, size);
            _occupiedAreas.Add(bounds);

            _obstacles.Add(obstacle);
        }
    }

    private void GenerateObstacles()
    {
        int obstacleCount = _obstacleSettings.GetObstacleCount(_currentLevel);
        float minSpeed = _obstacleSettings.GetMinSpeed(_currentLevel);
        float maxSpeed = _obstacleSettings.GetMaxSpeed(_currentLevel);

        int maxAttempts = obstacleCount * 5;
        int attemptCount = 0;
        int placedObstacles = 0;

        float xMin = Mathf.Min(LeftBound.x, RightBound.x);
        float xMax = Mathf.Max(LeftBound.x, RightBound.x);
        float yMin = Mathf.Min(TopBoundPoint.y, BottomBountPoint.y);
        float yMax = Mathf.Max(TopBoundPoint.y, BottomBountPoint.y);

        if (!(LevelController.Instance != null && LevelController.Instance.IsRestartingLevel))
            PersistentData.SavedObstacles.Clear();

        while (placedObstacles < obstacleCount && attemptCount < maxAttempts)
        {
            attemptCount++;

            var obstacleData = _obstacleSettings.GetRandomObstacleData();
            var prefab = obstacleData.ObstaclePrefab;
            if (prefab == null) continue;

            var pos = new Vector3(
                Random.Range(xMin, xMax),
                Random.Range(yMin, yMax),
                obstacleData.ZOffset);

            var approxSize = prefab.transform.localScale * obstacleData.SizeScale;
            var approxPadded = new Bounds(pos, approxSize + Vector3.one * _obstacleMinDistance);

            if (!IsAreaValid(approxPadded))
                continue;

            var obstacle = Instantiate(prefab, pos, Quaternion.Euler(obstacleData.Rotation));
            obstacle.transform.SetParent(transform);

            var rb2d = obstacle.GetComponent<Rigidbody2D>();
            float speed = Random.Range(minSpeed, maxSpeed);
            if (rb2d != null)
                rb2d.velocity = new Vector2(0f, -speed);

            var renderer = obstacle.GetComponentInChildren<Renderer>();
            var realSize = (renderer != null) ? renderer.bounds.size : approxSize;
            var realPadded = new Bounds(pos, realSize + Vector3.one * _obstacleMinDistance);

            if (!IsAreaValid(realPadded))
            {
                Destroy(obstacle);
                continue;
            }

            _occupiedAreas.Add(realPadded);
            SaveObstacle(obstacleData, prefab, pos, speed);
            _obstacles.Add(obstacle);
            placedObstacles++;
        }

        if (placedObstacles < obstacleCount)
            Debug.LogWarning($"[WallWithObstacles] Placed {placedObstacles}/{obstacleCount} (space constraints).");
    }

    private void ClearSceneObstacles()
    {
        var toRemove = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<IBrick>() == null)
                toRemove.Add(child.gameObject);
        }
        foreach (var go in toRemove)
            Destroy(go);

        _obstacles.Clear();
        _occupiedAreas.Clear();
    }

    private void ClearSavedStateIfNeeded()
    {
        if (LevelController.Instance == null || !LevelController.Instance.IsRestartingLevel)
            PersistentData.SavedObstacles.Clear();
    }

    private void SaveObstacle(ObstacleData obstacleData, GameObject obstaclePrefab, Vector3 position, float speed)
    {
        var state = new ObstacleState
        {
            Prefab = obstaclePrefab,
            Position = position,
            Rotation = Quaternion.Euler(obstacleData.Rotation),
            Speed = speed,
            SizeScale = obstacleData.SizeScale
        };

        PersistentData.SavedObstacles.Add(state);
    }

    private void RebuildOccupiedAreas()
    {
        _occupiedAreas.Clear();

        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            if (r == null) continue;
            if (r.TryGetComponent<IBrick>(out _)) continue;
            _occupiedAreas.Add(r.bounds);
        }
    }

    private bool IsAreaValid(Bounds newBounds)
    {
        foreach (var occupied in _occupiedAreas)
        {
            if (occupied.Intersects(newBounds))
                return false;
        }
        return true;
    }
}