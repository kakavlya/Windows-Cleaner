using DG.Tweening;
using UnityEngine;

namespace WindowsCleaner.UI
{
    public class CoinSizeDecrease : MonoBehaviour
    {
        [SerializeField] private float _startSize = 1f;
        [SerializeField] private float _delayBeforeDecrease = 0.5f;
        [SerializeField] private float _endSize = 0.2f;
        [SerializeField] private Ease _easingType = Ease.OutQuad;

        public void StartDecreaseSequence(float duration)
        {
            float durationToDecrease = duration - _delayBeforeDecrease;
            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(_delayBeforeDecrease);

            sequence.Append(transform.DOScale(_endSize, durationToDecrease).SetEase(_easingType))
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }
    }
}