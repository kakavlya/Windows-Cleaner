using System.Collections;
using UnityEngine;

public class WebGLPauseHandler : MonoBehaviour
{
    public static WebGLPauseHandler Instance;
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
                // Make sure audio is enabled at game start
                if (Audio.Instance != null)
                {
                    Audio.Instance.ToggleMusic(true);
                    Audio.Instance.ToggleSfx(true);
                }
                return;
            }
        }

        if (!hasFocus)
        {
            _pausedByFocus = true;
            PauseGame();
        }
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

    private UIStateMachine GetUIStateMachine()
    {
        if (_uiStateMachine == null)
        {
            _uiStateMachine = FindUiStateMachine();
        }
        return _uiStateMachine;
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
        // Only pause if we're in playing state
        if (GetUIStateMachine().GetCurrentState() == UIState.Playing)
        {
            GetUIStateMachine().SwitchState(UIState.PauseMenu);
            // Note: TimeScale is set in UIStateMachine.SwitchState
        }
    }
}
