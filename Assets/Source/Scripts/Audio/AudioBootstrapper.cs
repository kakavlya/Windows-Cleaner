using UnityEngine;

namespace WindowsCleaner.AudioNs
{
    public class AudioBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameObject _audioPrefab;

        private void Awake()
        {
            if (Audio.Instance == null)
            {
                Instantiate(_audioPrefab);
            }
        }
    }
}