using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
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
        //Debug.Log("Collided");
        if (collision.gameObject.GetComponent<Player>())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
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
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddTorque(transform.up * rotationH * rotationV * Random.Range(1.1f, 200f));
        GetComponent<Rigidbody>().AddTorque(transform.right * rotationH * rotationV * Random.Range(1.1f, 200f));
    }
}
