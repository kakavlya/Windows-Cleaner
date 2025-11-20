using UnityEngine;
using UnityEngine.Events;
using WindowsCleaner.WallNs;

namespace WindowsCleaner.PlayerNs
{
    public class MopController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _rotationSpeed = 3f;
        [SerializeField] private float _rotateAngle = 45f;
        [SerializeField] private float _rotationBackSpeed = 20f;

        [Header("Falling")]
        [SerializeField] private float _fallingSpeed = 1f;
        [SerializeField] private float _fallingMultiplier = 2f;

        [SerializeField] private Wall _wall;
        [SerializeField] private UnityEvent _outOfWallBoundaries;
        [SerializeField] private UnityEvent _insideWallBoundaries;

        [Header("Joystick Controls")]
        [SerializeField] private RectTransform _outerCircle;
        [SerializeField] private RectTransform _innerCircle;
        [SerializeField] private float _joystickSmoothness = 5f;

        private float _moveDirectionX;
        private Quaternion _originalRotation;
        private bool _isStopped;
        private Vector2 _startPos;

        private void Start()
        {
            _originalRotation = transform.rotation;

            if (_outerCircle != null)
            {
                _startPos = _outerCircle.position;
                if (_innerCircle != null)
                    _innerCircle.position = _startPos;
            }
        }

        private void Update()
        {
            if (_isStopped)
                return;

            HandleMove();
            HandleFalling();
            HandleRotation();
            ClampBoundsPosition();
        }

        public void Stop()
        {
            _isStopped = true;
            if (_innerCircle != null)
                _innerCircle.position = _startPos;
            _moveDirectionX = 0f;
        }

        private void HandleMove()
        {
            float axis = Input.GetAxis("Horizontal");
            _moveDirectionX = Mathf.Abs(axis) > Mathf.Abs(_moveDirectionX)
                ? axis
                : Mathf.Lerp(_moveDirectionX, axis, Time.deltaTime * _joystickSmoothness);

            HandleJoystickInput();

            Vector3 delta = new Vector3(_moveDirectionX * _moveSpeed * Time.deltaTime, 0f, 0f);
            transform.Translate(delta, Space.World);
        }

        private void HandleJoystickInput()
        {
            if (_outerCircle == null || _innerCircle == null)
                return;

            if (Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                Vector2 touchPos = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
                Vector2 direction = touchPos - _startPos;
                float maxDistance = _outerCircle.rect.width * 0.5f;

                float distance = Mathf.Clamp(direction.magnitude, 0f, maxDistance);
                direction = direction.sqrMagnitude > 0f ? direction.normalized : Vector2.zero;

                float target = (distance / maxDistance) * Mathf.Sign(direction.x);
                _moveDirectionX = Mathf.Lerp(_moveDirectionX, target, Time.deltaTime * _joystickSmoothness);

                _innerCircle.position = _startPos + direction * distance;
            }
            else
            {
                _innerCircle.position = _startPos;
                _moveDirectionX = Mathf.Lerp(_moveDirectionX, 0f, Time.deltaTime * _joystickSmoothness);
            }
        }

        private void HandleFalling()
        {
            Vector3 down = Vector3.down;
            bool outside = _wall != null && !_wall.CheckBoundaries(transform.position);

            if (outside)
            {
                down *= _fallingMultiplier;
                _outOfWallBoundaries?.Invoke();
            }
            else
            {
                _insideWallBoundaries?.Invoke();
            }

            transform.Translate(down * (_fallingSpeed * Time.deltaTime), Space.World);
        }

        private void HandleRotation()
        {
            if (Mathf.Abs(_moveDirectionX) > 0.0001f)
            {
                Quaternion target = Quaternion.Euler(
                    _originalRotation.eulerAngles.x,
                    _originalRotation.eulerAngles.y,
                    _rotateAngle * _moveDirectionX
                );
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _rotationSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, _originalRotation, Time.deltaTime * _rotationBackSpeed);
            }
        }

        private void ClampBoundsPosition()
        {
            if (_wall == null)
                return;

            float minX = Mathf.Min(_wall.LeftBound.x, _wall.RightBound.x);
            float maxX = Mathf.Max(_wall.LeftBound.x, _wall.RightBound.x);
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);

            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
}