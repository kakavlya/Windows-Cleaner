using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonBack : MonoBehaviour
{
    [SerializeField] private UIStateMachine _stateMachine;

    public void Click()
    {
        if (_stateMachine.GetCurrentState() == UIState.PauseMenu && WebGLPauseHandler.Instance != null)
        {
            _stateMachine.SwitchState(UIState.Playing);
        }
        else
        {
            _stateMachine.BackToPreviousState();
        }
    }
}
