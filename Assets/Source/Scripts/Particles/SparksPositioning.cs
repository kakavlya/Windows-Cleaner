using UnityEngine;
using WindowsCleaner.Obstacles;

namespace WindowsCleaner.Particles
{
    public class SparksPositioning : MonoBehaviour
    {
        [SerializeField] private MoveBetweenPoints mover;
        [SerializeField] private float _leftDirectionX;
        [SerializeField] private float _rightDirectionX;
        [SerializeField] private float _leftAngle;
        [SerializeField] private float _rightAngle;

        private void OnEnable()
        {
            if (mover != null)
            {
                mover.OnDirectionChanged += HandleDirectionChange;
            }
        }

        private void OnDisable()
        {
            if (mover != null)
            {
                mover.OnDirectionChanged -= HandleDirectionChange;
            }
        }

        private void HandleDirectionChange(MoveBetweenPoints.Direction newDirection)
        {
            switch (newDirection)
            {
                case MoveBetweenPoints.Direction.Left:
                    SetDirections(_leftDirectionX, _leftAngle);
                    break;
                case MoveBetweenPoints.Direction.Right:
                    SetDirections(_rightDirectionX, _rightAngle);
                    break;
            }
        }

        private void SetDirections(float posX, float angle)
        {
            var currPosition = transform.localPosition;
            currPosition.x = posX;
            transform.localPosition = currPosition;
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.y = angle;
            transform.eulerAngles = currentRotation;
        }
    }
}