using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Brick brick))
        {
            _player.BrickHit();
        }
        else if (other.TryGetComponent(out FinishObj finish))
        {
            _player.EndLevel();
        }
        else if(other.TryGetComponent(out Obstacle obstacle))
        {
            _player.Die();
        } 
    }
}
