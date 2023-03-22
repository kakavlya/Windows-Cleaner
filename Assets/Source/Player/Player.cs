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

    public void Die()
    {
        GameOver?.Invoke();
    }

    public void EndLevel()
    {
        WonLevel?.Invoke();
    }

    public void BrickHit()
    {
        IncrementScore?.Invoke();
    }
}
