using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : AbstractUIScreen
{

    public event UnityAction RestartButtonClick;
    public event UnityAction MainMenuButtonClick;

    protected override void OnButtonClick()
    {
        RestartButtonClick?.Invoke();
    }

    protected override void OnMainMenuButtonClick()
    {
        MainMenuButtonClick.Invoke();
    }
}
