using UnityEngine;
using WindowsCleaner.UI;

namespace WindowsCleaner.Misc
{
    public class WebGLPauseHandler : MonoBehaviour
    {
        private static WebGLPauseHandler _instance;
        private UIStateMachine _uiStateMachine;
        private bool _hasInitialized = false;

        public static WebGLPauseHandler Instance { get => _instance; private set => _instance = value; }

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

        public void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                PauseGame();
            }
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            if (!_hasInitialized)
            {
                if (hasFocus)
                {
                    _hasInitialized = true;
                    if (AudioNs.Audio.Instance != null)
                    {
                        AudioNs.Audio.Instance.ToggleMusic(true);
                        AudioNs.Audio.Instance.ToggleSfx(true);
                    }

                    return;
                }
            }

            if (!hasFocus)
            {
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

        private void PauseGame()
        {
            if (GetUIStateMachine().GetCurrentState() == UIState.Playing)
            {
                GetUIStateMachine().SwitchState(UIState.PauseMenu);
            }
        }
    }
}