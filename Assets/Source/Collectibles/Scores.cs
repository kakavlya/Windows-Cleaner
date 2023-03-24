using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Scores : MonoBehaviour
{
    public event UnityAction<float> ProgressUpdated;

    [SerializeField] private Player _player;
    [SerializeField] private Wall _wall;
    private int _totalBricksCount;
    private int _bricksHit;
    private float _currentScore;

    private void Start()
    {
        _bricksHit = 0;
        _currentScore = 0f;
        GetTotalBricksCount();
    }

    private void OnEnable()
    {
        _player.IncrementScore += UpdateScore;
    }

    private void OnDisable()
    {
        _player.IncrementScore -= UpdateScore;
    }

    private void UpdateScore()
    {
        _bricksHit++;
        GetTotalBricksCount();
        _currentScore = _bricksHit / (_totalBricksCount / 100f);
        ProgressUpdated?.Invoke(_currentScore);
    }

    private void GetTotalBricksCount()
    {
        _totalBricksCount = _wall.TotalBricks;
    }
}
