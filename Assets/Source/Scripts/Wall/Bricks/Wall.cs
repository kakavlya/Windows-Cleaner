using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public event Action WallGenerated;

    [SerializeField] private GameObject _brick;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 5;
    [SerializeField] private float _brickSpacing = 0.1f;

    private int _totalBricks;

    protected Vector3 LeftBoundPoint;
    protected Vector3 RightBoundPoint;
    protected Vector3 TopBoundPoint;
    protected Vector3 BottomBountPoint;

    public Vector3 LeftBound => LeftBoundPoint;
    public Vector3 RightBound => RightBoundPoint;
    public int TotalBricks => _totalBricks;

    protected virtual void Start()
    {
        GenerateWallUpdateTotal();
        CalculateTotalBricksAmount();
        WallGenerated?.Invoke();
    }

    public Vector3 GetTopCenterPoint()
    {
        float centerX = (LeftBoundPoint.x + RightBoundPoint.x) * 0.5f;
        float yTop = Mathf.Max(TopBoundPoint.y, BottomBountPoint.y);
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
                    BottomBountPoint = worldPos;
                    LeftBoundPoint = worldPos;
                }

                if (row == 0 && column == _columns - 1)
                    RightBoundPoint = worldPos;

                if (row == _rows - 1 && column == 0)
                    TopBoundPoint = worldPos;
            }
        }
    }

    public bool CheckBoundaries(Vector3 position)
    {
        float minX = Mathf.Min(LeftBoundPoint.x, RightBoundPoint.x);
        float maxX = Mathf.Max(LeftBoundPoint.x, RightBoundPoint.x);
        float minY = Mathf.Min(TopBoundPoint.y, BottomBountPoint.y);
        float maxY = Mathf.Max(TopBoundPoint.y, BottomBountPoint.y);

        return position.x >= minX && position.x <= maxX &&
               position.y >= minY && position.y <= maxY;
    }
}