using UnityEngine;
using WindowsCleaner.UI;

namespace WindowsCleaner.PlayerNs
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IBrick brick))
            {
                _player.BrickHit();
            }
            else if (other.TryGetComponent(out FinishObj finish))
            {
                _player.EndLevel();
            }
            else if (other.TryGetComponent(out Obstacle obstacle))
            {
                _player.Die();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out FinishObj finish))
            {

                _player.EndLevel();
            }
        }
    }
}