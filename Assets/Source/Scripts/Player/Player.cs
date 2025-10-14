using UnityEngine;
using UnityEngine.Events;

namespace WindowsCleaner.PlayerNs
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private MopController _mopController;
        [SerializeField] private AudioClip _collisionSound;

        public event UnityAction GameOver;
        public event UnityAction WonLevel;
        public event UnityAction IncrementScore;

        public void Die()
        {
            if (GameOver != null)
            {
                GameOver.Invoke();
                Audio.Instance?.PlaySfx(_collisionSound);
            }
        }

        public void EndLevel()
        {
            WonLevel?.Invoke();
            _mopController.Stop();
        }

        public void BrickHit()
        {
            IncrementScore?.Invoke();
        }
    }
}