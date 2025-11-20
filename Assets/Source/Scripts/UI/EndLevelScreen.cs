using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace WindowsCleaner.UI
{
    public class EndLevelScreen : AbstractUIScreen
    {
        [FormerlySerializedAs("_nextLvlButton")]
        [SerializeField] private Button _nextLvlButton;

        public event UnityAction NextButtonClick;
        public event UnityAction MainMenuButtonClick;
        public event UnityAction RestartButtonClick;

        protected override void OnEnable()
        {
            base.OnEnable();
            _nextLvlButton.onClick.AddListener(OnNextLvlBtnClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _nextLvlButton.onClick.RemoveListener(OnNextLvlBtnClick);
        }

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
    }
}