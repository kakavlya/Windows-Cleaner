using UnityEngine;
using UnityEngine.UI;

namespace WindowsCleaner.UI
{
    public abstract class AbstractUIScreen : MonoBehaviour
    {

        [SerializeField] protected Button Button;
        [SerializeField] protected Button MainMenuButton;
        protected abstract void OnButtonClick();
        protected abstract void OnMainMenuButtonClick();

        protected virtual void OnEnable()
        {
            Button.onClick.AddListener(OnButtonClick);
            MainMenuButton?.onClick.AddListener(OnMainMenuButtonClick);
        }

        protected virtual void OnDisable()
        {
            Button.onClick.RemoveListener(OnButtonClick);
            MainMenuButton?.onClick.RemoveListener(OnMainMenuButtonClick);
        }
    }
}