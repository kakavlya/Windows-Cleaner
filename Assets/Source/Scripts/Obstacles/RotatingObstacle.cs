using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{

    

    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 360f;

    private void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.Self);
    }
}
