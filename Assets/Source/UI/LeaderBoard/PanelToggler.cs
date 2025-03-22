using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelToggler : MonoBehaviour
{
    [SerializeField] private Button _ScreenOpenPanelcon;
    [SerializeField] private Button _panelClose;

    [SerializeField] private GameObject _UIPanel;

    private bool _wasGamePausedBefore;

    private void Start()
    {
        _ScreenOpenPanelcon.onClick.AddListener(ToggleLeaderBoard);
        _panelClose.onClick.AddListener(ToggleLeaderBoard);
    }

    private void OnDisable()
    {
        _ScreenOpenPanelcon.onClick.RemoveListener(ToggleLeaderBoard);
        _panelClose.onClick.RemoveListener(ToggleLeaderBoard);
    }

    private void ToggleLeaderBoard()
    {
        if (_UIPanel.activeSelf)
        {
            _UIPanel.SetActive(false);

            if (!_wasGamePausedBefore)
            {
                Time.timeScale = 1f;
            }
        }
        else
        {
            _UIPanel.SetActive(true);

            _wasGamePausedBefore = Time.timeScale == 0f;


            if (!_wasGamePausedBefore)
            {
                Time.timeScale = 0f;
            }
        }
    }
}
