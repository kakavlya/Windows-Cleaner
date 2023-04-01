using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private float rotationH = 500.5f;
    [SerializeField] private float rotationV = 1000.5f;
    [SerializeField] private float secondsToDestroy = 3f;
    [SerializeField] private ParticleSystem _particles;
    private void Start()
    {
        // Remove in prod
        AddRotation();
        _particles.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
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
            //Add particles with star, on hit
            _particles.Play();
        }
    }

    private void AddRotation()
    {
        //GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddTorque(transform.up * rotationH * rotationV * Random.Range(1.1f, 2f));
        GetComponent<Rigidbody>().AddTorque(transform.right * rotationH * rotationV * Random.Range(1.1f, 2f));
    }
}
