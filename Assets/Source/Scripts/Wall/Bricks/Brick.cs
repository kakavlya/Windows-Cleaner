using UnityEngine;
using WindowsCleaner.PlayerNs;

namespace WindowsCleaner.WallNs
{
    public class Brick : MonoBehaviour, IBrick
    {
        [SerializeField] private float rotationH = 500.5f;
        [SerializeField] private float rotationV = 1000.5f;
        [SerializeField] private float secondsToDestroy = 3f;

        private BoxCollider _collider;
        private Rigidbody _rigidbody;
        private void Start()
        {
            _collider = GetComponent<BoxCollider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Player>())
            {
                Destroy(gameObject);
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerCollisionHandler>())
            {

                Destroy(gameObject, secondsToDestroy);
                AddRotation();

                _collider.isTrigger = false;
                _rigidbody.AddForce(0, -3 * Random.Range(1.1f, 200f), 0);
            }
        }

        private void AddRotation()
        {
            _rigidbody.useGravity = true;
            _rigidbody.AddTorque(Random.Range(1.1f, 200f) * rotationH * rotationV * transform.up);
            _rigidbody.AddTorque(Random.Range(1.1f, 200f) * rotationH * rotationV * transform.right);
        }
    }
}