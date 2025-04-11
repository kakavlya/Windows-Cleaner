using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWithObjects : Wall
{
    [SerializeField] private GameObjectsSettings gameObjectsSettings; // Ссылка на настройки из ScriptableObject
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float objectMinDistance = 5.0f; // Минимальное расстояние между объектами

    private List<Bounds> _occupiedAreas = new List<Bounds>();

    protected override void Start()
    {
        base.Start();

        _occupiedAreas = GetOccupiedAreas();

        GenerateObjects(gameObjectsSettings.ObstaclesSettings);
        GenerateObjects(gameObjectsSettings.PickablesSettings);
    }

    private List<Bounds> GetOccupiedAreas()
    {
        List<Bounds> areas = new List<Bounds>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<IBrick>() != null)
                continue;
            Renderer rend = child.GetComponent<Renderer>();
            if (rend != null)
                areas.Add(rend.bounds);
        }
        return areas;
    }

    private bool IsAreaValid(Bounds newBounds)
    {
        foreach (var b in _occupiedAreas)
        {
            if (b.Intersects(newBounds))
                return false;
        }
        return true;
    }

    private void GenerateObjects(GameObjectTypeSettings settings)
    {
        if (settings == null)
            return;

        int count = settings.GetCount(currentLevel);
        float minSpeed = settings.GetMinSpeed(currentLevel);
        float maxSpeed = settings.GetMaxSpeed(currentLevel);

        for (int i = 0; i < count; i++)
        {
            ObstacleData data = settings.GetRandomData();
            if (data == null || data.ObstaclePrefab == null)
                continue;

            Vector3 randomPos = new Vector3(
                Random.Range(_leftBound.x, _rightBound.x),
                Random.Range(_bottomBound.y, _topBound.y),
                data.ZOffset);

            Bounds objBounds = new Bounds(randomPos, data.ObstaclePrefab.transform.localScale * data.SizeScale);

            if (IsAreaValid(objBounds))
            {
                GameObject obj = Instantiate(data.ObstaclePrefab, randomPos, Quaternion.Euler(data.Rotation), transform);
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float speed = Random.Range(minSpeed, maxSpeed);
                    rb.velocity = new Vector2(0, -speed);
                }
                _occupiedAreas.Add(objBounds);
            }
        }
    }
}
