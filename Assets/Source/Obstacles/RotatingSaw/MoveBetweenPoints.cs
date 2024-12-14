using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    public enum Direction { Left, Right}

    public event Action<Direction> OnDirectionChanged;

    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _pauseDuration = 1f;

    private Transform _targetPoint;
    private bool _isMoving = true;
    private float _pauseTimer = 0f;
    private Direction _currentDirection;

    private void Start()
    {
        if (_leftPoint == null || _rightPoint == null)
        {
            enabled = false;
            return;
        }

        _targetPoint = _rightPoint;
        _currentDirection = Direction.Right;
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPoint.position) < 0.1f)
            {
                _isMoving = false;
                _pauseTimer = _pauseDuration;
            }
        }
        else
        {
            _pauseTimer -= Time.deltaTime;
            if (_pauseTimer <= 0f)
            {
                if(_targetPoint == _leftPoint)
                {
                    _targetPoint = _rightPoint;
                    _currentDirection = Direction.Right;
                }
                else
                {
                    _targetPoint = _leftPoint;
                    _currentDirection = Direction.Left;
                }
                _isMoving = true;

                OnDirectionChanged?.Invoke(_currentDirection);
            }
        }
    }
}
