using System;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        this.transform.position = _target.position;
        //this.transform.rotation = _target.rotation;
    }
}