using System;
using UnityEngine;

namespace WindowsCleaner.PlayerNs
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            this.transform.position = _target.position;
        }
    }
}