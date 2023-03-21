using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    
    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.GetComponent<CollidingMop>())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Collided");
        if (other.GetComponent<CollidingMop>())
        {
            Destroy(gameObject);
        }
    }
}
