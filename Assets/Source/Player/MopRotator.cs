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
    [SerializeField] private float _minClamp;
    [SerializeField] private float _maxClamp;


    private float _originalRotation;
    private float _currentRotation;
    private float _rotation;
    private float _newBearing;
    private float _currentBearing;

    private void Start()
    {
        _originalRotation = transform.rotation.z;
    }

    private void FixedUpdate()
    {
        float moveInput = _playerMover.MoveInput().x;
        if(moveInput != 0)
        {
            _rotation = moveInput * (Time.deltaTime + _rotationSpeed);
            //transform.Rotate(0, 0, _rotation);
            _newBearing = _currentBearing + _rotation;
            SetCurrentRotation(_newBearing);
        } else
        {
            RotateBack();
        }
    }

    private void SetCurrentRotation(float rot)
    {
        _currentRotation = Mathf.Clamp(rot, _minClamp, _maxClamp);
        transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
    }
    private void RotateBack()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, _originalRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationBackSpeed);
    }

}
