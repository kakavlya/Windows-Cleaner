using System.Collections.Generic;
using UnityEngine;
using WindowsCleaner.Obstacles;

namespace WindowsCleaner.WallNs
{
    public class WallWithObjects : Wall
    {
        private readonly List<Bounds> _occupiedAreas = new List<Bounds>();

        [SerializeField] private GameObjectsSettings _gameObjectsSettings;
        [SerializeField] private int _currentLevel = 1;
        [SerializeField] private float _objectMinDistance = 5f;

        protected override void Start()
        {
            base.Start();
            _occupiedAreas.Clear();
            RebuildOccupiedAreas();

            GenerateObjects(_gameObjectsSettings.ObstaclesSettings);
            GenerateObjects(_gameObjectsSettings.PickablesSettings);
        }

        private void GenerateObjects(GameObjectTypeSettings settings)
        {
            if (settings == null)
            {
                return;
            }

            int count = settings.GetCount(_currentLevel);
            float minSpeed = settings.GetMinSpeed(_currentLevel);
            float maxSpeed = settings.GetMaxSpeed(_currentLevel);

            float xMin = Mathf.Min(LeftBoundPoint.x, RightBoundPoint.x);
            float xMax = Mathf.Max(LeftBoundPoint.x, RightBoundPoint.x);
            float yMin = Mathf.Min(BottomBountPoint.y, TopBoundPoint.y);
            float yMax = Mathf.Max(BottomBountPoint.y, TopBoundPoint.y);

            int attempts = 0;
            int maxAttempts = count * 5;
            int placed = 0;

            while (placed < count && attempts < maxAttempts)
            {
                attempts++;

                var data = settings.GetRandomData();
                var prefab = data?.ObstaclePrefab;

                if (prefab == null)
                {
                    continue;
                }

                var pos = new Vector3(
                    Random.Range(xMin, xMax),
                    Random.Range(yMin, yMax),
                    data.ZOffset);

                var approxSize = prefab.transform.localScale * data.SizeScale;
                var approxPadded = new Bounds(pos, approxSize + (Vector3.one * _objectMinDistance));

                if (!IsAreaValid(approxPadded))
                {
                    continue;
                }

                var obj = Instantiate(prefab, pos, Quaternion.Euler(data.Rotation), transform);

                var rb2d = obj.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    float speed = Random.Range(minSpeed, maxSpeed);
                    rb2d.velocity = new Vector2(0f, -speed);
                }

                var renderer = obj.GetComponentInChildren<Renderer>();
                var realSize = renderer != null ? renderer.bounds.size : approxSize;
                var realPadded = new Bounds(pos, realSize + (Vector3.one * _objectMinDistance));

                if (!IsAreaValid(realPadded))
                {
                    Destroy(obj);
                    continue;
                }

                _occupiedAreas.Add(realPadded);
                placed++;
            }

            if (placed < count)
            {
                Debug.LogWarning($"[WallWithObjects] Placed {placed}/{count} (space constraints).");
            }
        }

        private void RebuildOccupiedAreas()
        {
            _occupiedAreas.Clear();

            var renderers = GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                if (r == null)
                {
                    continue;
                }

                if (r.TryGetComponent<IBrick>(out _))
                {
                    continue;
                }

                _occupiedAreas.Add(r.bounds);
            }
        }

        private bool IsAreaValid(Bounds newBounds)
        {
            foreach (var b in _occupiedAreas)
            {
                if (b.Intersects(newBounds))
                {
                    return false;
                }
            }

            return true;
        }
    }
}