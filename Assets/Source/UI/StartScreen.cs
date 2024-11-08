using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : AbstractUIScreen
{
    public event UnityAction StartButtonClick;

    protected override void OnButtonClick()
    {
        StartButtonClick?.Invoke();
    }

    protected override void OnMainMenuButtonClick()
    {
        
    }
}
