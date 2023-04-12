using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    public event UnityAction GameOver;
    public event UnityAction WonLevel;
    public event UnityAction IncrementScore;
    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    public void Die()
    {
        GameOver?.Invoke();
    }

    public void EndLevel()
    {
        WonLevel?.Invoke();
        _playerMover.Stop();
    }

    public void BrickHit()
    {
        IncrementScore?.Invoke();
    }
}
