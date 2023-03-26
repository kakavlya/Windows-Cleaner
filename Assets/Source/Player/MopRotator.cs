using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MopRotator : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private float _rotationDegree;
    [SerializeField] private float _minClamp;
    [SerializeField] private float _maxClamp;


    private float _currentRotation;
    private float _rotation;
    private float _newBearing;
    private float _currentBearing;

    private void FixedUpdate()
    {
        float moveInput = _playerMover.MoveInput().x;
        _rotation = moveInput * (Time.deltaTime + _rotationDegree);
        transform.Rotate(0, 0, _rotation);
        _newBearing = _currentBearing + _rotation;
        SetCurrentRotation(_newBearing);
    }

    private void SetCurrentRotation(float rot)
    {
        _currentRotation = Mathf.Clamp(rot, _minClamp, _maxClamp);
        transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
    }
}
