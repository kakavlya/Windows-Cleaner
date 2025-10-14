using UnityEngine.Events;

namespace WindowsCleaner.UI
{
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
}