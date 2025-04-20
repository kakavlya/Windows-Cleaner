using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonStateSelector : MonoBehaviour
{
    [SerializeField] private UIState _state;
    [SerializeField] private UIStateMachine _uiStateMachine;

    public void OnClick()
    {
        _uiStateMachine.SwitchState(_state);
    }
}
