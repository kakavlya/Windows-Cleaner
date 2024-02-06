using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _verticalSpeed = 7f;
    [SerializeField] private Wall _wall;
    private float _yDirection = -1f;
    private Vector3 _moveDirection;
    private Vector2 _moveInput;
    private bool _stopped;

    private void Start()
    {
        _stopped = false;
    }

    private void LateUpdate()
    {
        Move();

        //CheckBoundaries();
    }

    private void Move()
    {
        if (_stopped)
        {
            return;
        }
        _moveDirection = new(_moveInput.x, _yDirection, 0);
        var nextPosition = _moveDirection * _verticalSpeed * Time.deltaTime;

        transform.Translate(nextPosition);
    }

    private void CheckBoundaries()
    {
        var adjustedPosition = this.transform.position;
        adjustedPosition.x = Math.Clamp(this.transform.position.x, _wall.LeftBound.x, _wall.RightBound.x);

        this.transform.position = adjustedPosition;
    }

    public Vector2 MoveInput()
    {
        return _moveInput;
    }

    public void OnMovement(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void Stop()
    {
        _stopped = true;
    }
}
