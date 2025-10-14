using UnityEngine;
using DG.Tweening;

namespace WindowsCleaner.UI
{
    public class BrickDebrisController : MonoBehaviour
    {
        [SerializeField] private Vector2 _lifetime = new Vector2(0.9f, 1.5f);
        [SerializeField] private Vector2 _xOffset = new Vector2(-7.5f, 7.5f);
        [SerializeField] private Vector2 _yOffset = new Vector2(-4f, -19f);
        [SerializeField] private Vector2 _zOffset = new Vector2(-2.5f, 2.5f);

        public void Launch()
        {
            float lifeTime = Random.Range(_lifetime.x, _lifetime.y);
            Vector3 randomOffset = new Vector3(
                  Random.Range(_xOffset.x, _xOffset.y),
                  Random.Range(_yOffset.x, _yOffset.y),
                  Random.Range(_zOffset.x, _zOffset.y)
              );

            Vector3 randomRotation = new Vector3(
                Random.Range(-180f, 180f),
                Random.Range(-180f, 180f),
                Random.Range(-180f, 180f)
            );

            Sequence sequence = DOTween.Sequence();

            sequence.Append(
                transform.DOMove(transform.position + randomOffset, lifeTime)
                         .SetEase(Ease.Linear)
                         .SetUpdate(true)
            );

            sequence.Join(
                transform.DORotate(randomRotation, lifeTime, RotateMode.FastBeyond360)
                         .SetEase(Ease.InCubic)
                         .SetUpdate(true)
            );

            sequence.OnComplete(() => Destroy(gameObject));
        }
    }
}