using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndLevelScreen : AbstractUIScreen
{
    public event UnityAction NextButtonClick;
    public event UnityAction RestartButtonClick;

    protected override void OnButtonClick()
    {
        RestartButtonClick.Invoke();
    }
}
