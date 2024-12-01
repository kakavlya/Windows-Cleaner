using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{

    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _pauseDuration = 1f;
    [SerializeField] private float _rotationSpeed = 360f;

    private Transform _targetPoint;
    private bool _isMoving = true;
    private float _pauseTimer = 0f;

    private void Start()
    {
        if (_pointA == null || _pointB == null)
        {
            enabled = false;
            return;
        }

        _targetPoint = _pointB;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.Self);

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
            if(_pauseTimer <= 0f)
            {
                _targetPoint = _targetPoint == _pointA ? _pointB : _pointA;
                _isMoving = true;
            }
        }
    }
}
