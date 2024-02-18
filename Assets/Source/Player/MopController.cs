using System;
using UnityEngine;

public class MopController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _rotateAngle = 45f;
    [SerializeField] private float _rotationBackSpeed = 20f;


    [Header("Falling")]
    [SerializeField] private float _fallinSpeed = 1f;



    private float _moveDirectionX;
    private Quaternion _originalRotation;


    private void Start()
    {
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        
      
    }

    private void FixedUpdate()
    {
        HandleMove();
        HandleFalling();
    }


    private void LateUpdate()
    {
        HandleRotation();
    }

    private void HandleMove()
    {
        _moveDirectionX = Input.GetAxis("Horizontal");
        var vector = this.transform.up;
        if (_moveDirectionX > 0)
        {
            vector *= -1;

        }

        this.transform.Translate(vector * (_moveDirectionX * Time.deltaTime * _moveSpeed));
    }

    private void HandleFalling()
    {
        var downVector = this.transform.up * -1f;
        this.transform.Translate(downVector * (_fallinSpeed * Time.deltaTime));
    }

    private void HandleRotation()
    {
        if (_moveDirectionX != 0)
        {
            var _targetRotation = Quaternion.Euler(_originalRotation.eulerAngles.x, _originalRotation.eulerAngles.y, _rotateAngle * _moveDirectionX);

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