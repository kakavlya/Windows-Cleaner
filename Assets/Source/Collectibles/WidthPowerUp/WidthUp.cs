using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidthUp : MonoBehaviour
{
    [SerializeField] private float _widthBuff;

    private void Start()
    {
        _widthBuff = _widthBuff == 0 ? 1.2f : _widthBuff;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Collided");
        if (other.GetComponent<Player>())
        {
            Remove();
            var localXUpdated = new Vector3(other.transform.localScale.x * _widthBuff, 
                other.transform.localScale.y, other.transform.localScale.z);
            other.transform.localScale = localXUpdated;
        }
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}
