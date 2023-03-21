using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCollisionHandler : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.TryGetComponent(out FinishObj finish))
        {
            _player.EndLevel();
        }
        else if(other.TryGetComponent(out Obstacle obstacle))
        {
            _player.Die();
        }
    }
}
