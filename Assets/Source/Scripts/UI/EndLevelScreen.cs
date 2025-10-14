using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WindowsCleaner.UI
{
    public class EndLevelScreen : AbstractUIScreen
    {
        [SerializeField] private Button NextLvlButton;

        public event UnityAction NextButtonClick;
        public event UnityAction MainMenuButtonClick;
        public event UnityAction RestartButtonClick;

        protected override void OnButtonClick()
        {
            RestartButtonClick.Invoke();
        }

        protected override void OnMainMenuButtonClick()
        {
            MainMenuButtonClick.Invoke();
        }

        public void OnNextLvlBtnClick()
        {
            NextButtonClick.Invoke();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            NextLvlButton.onClick.AddListener(OnNextLvlBtnClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            NextLvlButton.onClick.RemoveListener(OnNextLvlBtnClick);
        }
    }
}