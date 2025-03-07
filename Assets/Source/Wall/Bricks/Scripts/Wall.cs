using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public event Action WallGenerated;

    [SerializeField] private GameObject _brick;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 5;
    [SerializeField] private float _brickSpacing = 0.1f;

    private int _totalBricks;

    protected Vector3 _leftBound;
    protected Vector3 _rightBound;

    protected Vector3 _topBound;
    protected Vector3 _bottomBound;


    public Vector3 LeftBound => _leftBound;
    public Vector3 RightBound => _rightBound;
    public int TotalBricks { get => _totalBricks;}

    protected virtual void Start()
    {
        GenerateWallUpdateTotal();
        CalculateTotalBricksAmount();

        WallGenerated?.Invoke();
    }

    public Vector3 GetTopCenterPoint()
    {
        float centerX = (_leftBound.x + _rightBound.x) / 2;
        float centerY = _topBound.y;
        //Debug.Log("Topbound.y: " + _topBound.y);
        //Debug.Log("BottomBound.y: " + _bottomBound.y);
        return new Vector3(centerX, centerY, 0);
    }

    private void CalculateTotalBricksAmount()
    {
        _totalBricks = (_rows * _columns);
    }

    private void GenerateWallUpdateTotal()
    {
        for(int row = 0; row < _rows; row++)
        {
            
            for(int column = 0; column < _columns; column++)
            {
                if(_brick != null)
                {
                    Vector3 position = new Vector3(column * (_brick.transform.localScale.x + _brickSpacing),
                                                row * (_brick.transform.localScale.y + _brickSpacing),
                                                0);
                    GameObject brick = Instantiate(_brick, position, Quaternion.identity);

                    if (row == 0)
                    {
                        _bottomBound = brick.transform.position;
                        if (column == 0)
                        {
                            _leftBound = brick.transform.position;
                        } else if (column == _columns - 1)
                        {
                            _rightBound = brick.transform.position;
                        }
                    } else if (row == _rows - 1)
                    {
                        _topBound = brick.transform.position;

                    }
                    brick.transform.SetParent(transform);
                }
            }
        }
    }

    public bool CheckBoundaries(Vector3 position)
    {
        return position.x >= LeftBound.x && position.x <= RightBound.x && position.y  <= _bottomBound.y && position.y >= _topBound.y;
    }
}
