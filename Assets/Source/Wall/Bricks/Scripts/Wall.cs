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
                    brick.transform.SetParent(transform);
                }
            }
        }
    }
}
