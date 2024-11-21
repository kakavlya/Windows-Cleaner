using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardToggler : MonoBehaviour
{
    [SerializeField] private Button _leaderBoardIcon;
    [SerializeField] private Button _leaderBoardClose;

    [SerializeField] private GameObject _leaderBoard;

    private bool _wasGamePausedBefore;

    private void Start()
    {
        _leaderBoardIcon.onClick.AddListener(ToggleLeaderBoard);
        _leaderBoardClose.onClick.AddListener(ToggleLeaderBoard);
    }

    private void OnDisable()
    {
        _leaderBoardIcon.onClick.RemoveListener(ToggleLeaderBoard);
        _leaderBoardClose.onClick.RemoveListener(ToggleLeaderBoard);
    }

    private void ToggleLeaderBoard()
    {
        if (_leaderBoard.activeSelf)
        {
            _leaderBoard.SetActive(false);

            if (!_wasGamePausedBefore)
            {
                Time.timeScale = 1f;
            }
        }
        else
        {
            _leaderBoard.SetActive(true);

            _wasGamePausedBefore = Time.timeScale == 0f;


            if (!_wasGamePausedBefore)
            {
                Time.timeScale = 0f;
            }
        }
    }
}
