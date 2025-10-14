using UnityEngine;
using UnityEngine.InputSystem;
using WindowsCleaner.UI;

namespace WindowsCleaner.PlayerNs
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _horizontalSpeed = 5f;
        [SerializeField] private float _verticalSpeed = 7f;
        [SerializeField] private Wall _wall;
        private readonly float _yDirection = -1f;
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
        }

        private void Move()
        {
            if (_stopped)
            {
                return;
            }

            _moveDirection = new(_moveInput.x, _yDirection, 0);
            var nextPosition = _verticalSpeed * Time.deltaTime * _moveDirection;

            transform.Translate(nextPosition);
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
}