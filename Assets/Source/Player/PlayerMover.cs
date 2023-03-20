using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 5f;
    [SerializeField] private float _verticalSpeed = 7f;

    private Vector2 _moveInput;

    private void Update()
    {
        transform.Translate(_moveInput * _horizontalSpeed * Time.deltaTime);
        Vector3 downMovement = new Vector3(0, -1, 0);
        transform.Translate(downMovement * _verticalSpeed * Time.deltaTime);
    }

    public void OnMovement(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        Debug.Log("On Move value: " + value.Get<Vector2>());
    }
}
