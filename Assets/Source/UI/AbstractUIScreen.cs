using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractUIScreen : MonoBehaviour
{

    [SerializeField] protected Button Button;
    protected abstract void OnButtonClick();

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClick);
    }
}
