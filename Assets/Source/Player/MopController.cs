using System;
using UnityEngine;
using UnityEngine.Events;

public class MopController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _rotateAngle = 45f;
    [SerializeField] private float _rotationBackSpeed = 20f;


    [Header("Falling")]
    [SerializeField] private float _fallinSpeed = 1f;
    [SerializeField] private float _fallinMultiplayer = 2f;
    [SerializeField] private Wall _wall;
    [SerializeField] private UnityEvent _outOfWallBoundaries;
    [SerializeField] private UnityEvent _insideWallBoundaris;

    [Header("Joystick Controls")]
    [SerializeField] private RectTransform _outerCircle;
    [SerializeField] private RectTransform _innerCircle;
    [SerializeField] private float _joystickSmoothness = 5f;

    private float _moveDirectionX;
    private Quaternion _originalRotation;
    private bool _isStopped;
    private Vector2 _startPos;
    private Vector2 _inputDir;

    private void Start()
    {
        _originalRotation = transform.rotation;
        _startPos = _outerCircle.position;
        _innerCircle.position = _startPos;
    }

    private void FixedUpdate()
    {
        if (_isStopped == false)
        {
            HandleRotation();
            HandleMove();
            HandleFalling();
        }
       
    }


    private void LateUpdate()
    {
        if (_isStopped == false)
        {
            //HandleRotation();

            
        }
  
    }

    private void HandleMove()
    {
        // Keyboard movement
        _moveDirectionX = Input.GetAxis("Horizontal");
        var vector = this.transform.up - Vector3.down;
        if (_moveDirectionX > 0)
        {
            //Debug.Log($"Vector: {vector}");
            vector *= -1;
        }
        this.transform.Translate(vector * (_moveDirectionX * Time.deltaTime * _moveSpeed));

        // Joystick movement
        HandleJoystickInput();
    }
    private void HandleJoystickInput()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Vector2 touchPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            // Calculate the direction from the center of the outer circle to the touch position
            Vector2 direction = touchPos - _startPos;
            float maxDistance = _outerCircle.rect.width / 2;

            // Clamp the distance to the maxDistance and calculate normalized direction
            float distance = Mathf.Clamp(direction.magnitude, 0, maxDistance);
            direction.Normalize();

            // Scale move direction based on the distance as a factor (provides smoother control)
            float targetDirectionX = (distance / maxDistance) * Mathf.Sign(direction.x);

            // Smoothly interpolate _moveDirectionX towards the target direction for a gradual effect
            _moveDirectionX = Mathf.Lerp(_moveDirectionX, targetDirectionX, Time.deltaTime * _joystickSmoothness);

            // Set inner circle position for visual feedback, clamped to max distance
            _innerCircle.position = _startPos + direction * distance;
        }
        else
        {
            // Reset inner circle to center and smoothly bring _moveDirectionX back to zero
            _innerCircle.position = _startPos;
            _moveDirectionX = Mathf.Lerp(_moveDirectionX, 0f, Time.deltaTime * _joystickSmoothness);
        }
    }

    private void HandleJoystickMove()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Vector2 touchPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
            _inputDir = (touchPos - _startPos).normalized;

            float distance = Vector2.Distance(touchPos, _startPos);
            float maxDistance = _outerCircle.rect.width / 2;

            // Move inner circle within bounds
            _innerCircle.position = _startPos + Vector2.ClampMagnitude(touchPos - _startPos, maxDistance);

            if (distance > maxDistance)
                distance = maxDistance;

            Vector3 moveVector = new Vector3(_inputDir.x, 0, _inputDir.y) * (_moveSpeed * (distance / maxDistance) * Time.deltaTime);
            transform.Translate(moveVector, Space.World);
        }
        else
        {
            _innerCircle.position = _startPos;
            _inputDir = Vector2.zero;
        }
    }

    private void HandleFalling()
    {
        var downVector = Vector3.down;
        if (_wall && _wall.CheckBoundaries(this.transform.position) == false)
        {
            downVector *= _fallinMultiplayer;
            this._outOfWallBoundaries?.Invoke();
        }
        else
        {
            this._insideWallBoundaris?.Invoke();
        }
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

    public void Stop()
    {
        _isStopped = true;
    }
}