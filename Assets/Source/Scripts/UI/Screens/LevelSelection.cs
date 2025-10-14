using UnityEngine;
using UnityEngine.UI;

namespace WindowsCleaner.UI
{
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField] private Button _levelSelection;
        [SerializeField] private GameObject _levelsMenu;

        private void Start()
        {
            _levelSelection.onClick.AddListener(OpenMenu);
        }

        private void OnDisable()
        {
            _levelSelection.onClick.RemoveListener(OpenMenu);
        }

        private void OpenMenu()
        {
            _levelsMenu.SetActive(true);
        }
    }
}