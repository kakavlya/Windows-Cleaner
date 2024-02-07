using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MopRotator : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private float _rotationDegree;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationBackSpeed = 20f;


    private Quaternion _originalRotation;

    private void Start()
    {
        _originalRotation = transform.rotation;

    }

    private void FixedUpdate()
    {
        
        float moveInput = _playerMover.MoveInput().x;

        if (moveInput != 0)
        {
            var _targetRotation = Quaternion.Euler(_originalRotation.eulerAngles.x, _originalRotation.eulerAngles.y, _rotationDegree * moveInput);

            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);

        } else
        {
            RotateBack();
        }
    }


    private void RotateBack()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _originalRotation, Time.deltaTime * _rotationBackSpeed);
    }

}
