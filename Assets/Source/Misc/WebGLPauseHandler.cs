using System.Collections;
using UnityEngine;

public class WebGLPauseHandler : MonoBehaviour
{
    public static WebGLPauseHandler Instance;
    //private PauseScreen _pauseMenu;
    private UIStateMachine _uiStateMachine;
    private bool _hasInitialized = false;
    private bool _pausedByFocus = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (!_hasInitialized)
        {
            if (hasFocus)
            {
                _hasInitialized = true;
                return;
            }
        }

        if (!hasFocus)
        {
            _pausedByFocus = true;
            PauseGame();
        }
        else
        {
            if (!_pausedByFocus)
            {
                Debug.Log("Focus returned, but not paused by focus. Skipping UI.");
                return;
            }

            _pausedByFocus = false;
            ActivatePauseMenuDelayed();
        }
    }

    private void ActivatePauseMenuDelayed()
    {
        if(_uiStateMachine == null)
        {
            _uiStateMachine = FindUiStateMachine();
        }
        _uiStateMachine.SwitchState(UIState.PauseMenu);
    }

    private UIStateMachine FindUiStateMachine()
    {
        var allUiStateMachines = Resources.FindObjectsOfTypeAll<UIStateMachine>();
        foreach (var uiStateMachine in allUiStateMachines)
        {
            if (uiStateMachine.gameObject.scene.IsValid())
            {
                return uiStateMachine;
            }
        }

        return null;
    }

    public void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        ToggleAudio(false);
    }

    public void ResumeGame()
    {
        _uiStateMachine.SwitchState(UIState.Playing);
        Time.timeScale = 1f;
        ToggleAudio(true);
    }

    private void ToggleAudio(bool enabled)
    {
        if (Audio.Instance != null)
        {
            Audio.Instance.ToggleMusic(enabled);
            Audio.Instance.ToggleSfx(enabled);
        }
    }
}
