using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject _brick;
    [SerializeField] private int _rows = 5;
    [SerializeField] private int _columns = 5;
    [SerializeField] private float _brickSpacing = 0.1f;

    private int _totalBricks;

    private Vector3 _leftBound;
    private Vector3 _rightBound;

    public Vector3 LeftBound => _leftBound;
    public Vector3 RightBound => _rightBound;
    public int TotalBricks { get => _totalBricks;}

    private void Start()
    {
        GenerateWallUpdateTotal();
        CalculateTotalBricksAmount();
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
                        if (column == 0)
                        {
                            _leftBound = brick.transform.position;
                        } else if (column == _columns - 1)
                        {
                            _rightBound = brick.transform.position;
                        }
                    }
                    brick.transform.SetParent(transform);
                }
            }
        }
    }
}
