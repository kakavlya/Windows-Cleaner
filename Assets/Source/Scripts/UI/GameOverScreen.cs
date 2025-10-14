using UnityEngine.Events;

namespace WindowsCleaner.UI
{
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
}