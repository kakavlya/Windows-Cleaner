using UnityEngine;

namespace WindowsCleaner.UI
{
    public class UIButtonStateSelector : MonoBehaviour
    {
        [SerializeField] private UIState _state;
        [SerializeField] private UIStateMachine _uiStateMachine;

        public void OnClick()
        {
            _uiStateMachine.SwitchState(_state);
        }
    }
}