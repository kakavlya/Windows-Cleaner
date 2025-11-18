using System;
using UnityEngine;

namespace WindowsCleaner.WallNs
{
    public class Wall : MonoBehaviour
    {
        public event Action WallGenerated;

        [SerializeField] private GameObject _brick;
        [SerializeField] private int _rows = 5;
        [SerializeField] private int _columns = 5;
        [SerializeField] private float _brickSpacing = 0.1f;

        private int _totalBricks;

        private Vector3 _leftBoundPoint;
        private Vector3 _rightBoundPoint;
        private Vector3 _topBoundPoint;
        private Vector3 _bottomBoundPoint;

        public Vector3 LeftBound => _leftBoundPoint;
        public Vector3 RightBound => _rightBoundPoint;
        public Vector3 TopBound => _topBoundPoint;
        public Vector3 BottomBound => _bottomBoundPoint;

        public int TotalBricks => _totalBricks;

        protected virtual void Start()
        {
            GenerateWallUpdateTotal();
            CalculateTotalBricksAmount();
            WallGenerated?.Invoke();
        }

        public Vector3 GetTopCenterPoint()
        {
            float centerX = (LeftBound.x + RightBound.x) * 0.5f;
            float yTop = Mathf.Max(TopBound.y, BottomBound.y);
            return new Vector3(centerX, yTop, 0f);
        }

        private void CalculateTotalBricksAmount()
        {
            _totalBricks = _rows * _columns;
        }

        private void GenerateWallUpdateTotal()
        {
            if (_brick == null || _rows <= 0 || _columns <= 0)
                return;

            float cellW = _brick.transform.localScale.x + _brickSpacing;
            float cellH = _brick.transform.localScale.y + _brickSpacing;

            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    Vector3 localPos = new Vector3(column * cellW, row * cellH, 0f);
                    GameObject brick = Instantiate(_brick, transform);
                    brick.transform.localPosition = localPos;

                    Vector3 worldPos = brick.transform.position;

                    if (row == 0 && column == 0)
                    {
                        _bottomBoundPoint = worldPos;
                        _leftBoundPoint = worldPos;
                    }

                    if (row == 0 && column == _columns - 1)
                        _rightBoundPoint = worldPos;

                    if (row == _rows - 1 && column == 0)
                        _topBoundPoint = worldPos;
                }
            }
        }

        public bool CheckBoundaries(Vector3 position)
        {
            float minX = Mathf.Min(LeftBound.x, RightBound.x);
            float maxX = Mathf.Max(LeftBound.x, RightBound.x);
            float minY = Mathf.Min(TopBound.y, BottomBound.y);
            float maxY = Mathf.Max(TopBound.y, BottomBound.y);

            return position.x >= minX && position.x <= maxX &&
                   position.y >= minY && position.y <= maxY;
        }
    }
}