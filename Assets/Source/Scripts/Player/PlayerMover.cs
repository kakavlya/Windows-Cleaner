using UnityEngine;
using UnityEngine.InputSystem;
using WindowsCleaner.WallNs;

namespace WindowsCleaner.PlayerNs
{
    public class PlayerMover : MonoBehaviour
    {
        private readonly float _yDirection = -1f;

        [SerializeField] private float _horizontalSpeed = 5f;
        [SerializeField] private float _verticalSpeed = 7f;
        [SerializeField] private Wall _wall;

        private Vector3 _moveDirection;
        private Vector2 _moveInput;

        private void LateUpdate()
        {
            Move();
        }

        public Vector2 MoveInput()
        {
            return _moveInput;
        }

        public void OnMovement(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        private void Move()
        {
            _moveDirection = new (_moveInput.x, _yDirection, 0);
            var nextPosition = _verticalSpeed * Time.deltaTime * _moveDirection;

            transform.Translate(nextPosition);
        }
    }
}