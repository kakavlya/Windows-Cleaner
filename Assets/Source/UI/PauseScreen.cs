using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        WebGLPauseHandler.Instance.ResumeGame();
        _panel.SetActive(false);
    }

    public void ActivatePanel()
    {
        _panel.SetActive(true);
    }
}
