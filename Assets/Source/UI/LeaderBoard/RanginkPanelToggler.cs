using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanginkPanelToggler : PanelToggler
{
    [SerializeField] private CanvasGroup _sidePanelCanvasGroup;
    protected override void TogglePanel()
    {
        base.TogglePanel();

        bool isVisible = _sidePanelCanvasGroup.alpha > 0f;
        SetPanelVisible(!isVisible);
    }

    private void SetPanelVisible(bool visible)
    {
        _sidePanelCanvasGroup.alpha = visible ? 1f : 0f;
        _sidePanelCanvasGroup.interactable = visible;
        _sidePanelCanvasGroup.blocksRaycasts = visible;
    }
}
