using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _verticalSpeed = 7f;
    private float _yDirection = -1f;
    private Vector3 _moveDirection;
    private Vector2 _moveInput;
    private bool _stopped;

    private void Start()
    {
        _stopped = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_stopped)
        {
            return;
        }
        _moveDirection = new(_moveInput.x, _yDirection, 0);
        transform.Translate(_moveDirection * _verticalSpeed * Time.deltaTime);
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
