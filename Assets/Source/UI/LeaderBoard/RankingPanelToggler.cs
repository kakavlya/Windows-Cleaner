using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingPanelToggler : PanelToggler
{
    [SerializeField] private CanvasGroup[] _sidePanelCanvasGroups;
    protected override void TogglePanel()
    {
        base.TogglePanel();

        SetPanelsVisible();
        
    }

    private void SetPanelsVisible()
    {
        foreach(var canvasGroup in _sidePanelCanvasGroups)
        {
            bool isVisible = canvasGroup.alpha > 0f;
            SetPanelVisible(!isVisible, canvasGroup);
        }
    }

    private void SetPanelVisible(bool visible, CanvasGroup panel)
    {
        panel.alpha = visible ? 1f : 0f;
        panel.interactable = visible;
        panel.blocksRaycasts = visible;
    }
}
