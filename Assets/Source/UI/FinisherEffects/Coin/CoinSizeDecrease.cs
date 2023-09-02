using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSizeDecrease : MonoBehaviour
{
    [SerializeField] private float _startSize = 1f;
    [SerializeField] private float delayBeforeDecrease = 0.5f;
    [SerializeField] private float _endSize = 0.2f;
    [SerializeField] private Ease easingType = Ease.OutQuad;

    public void StartDecreaseSequence(float duration)
    {
        float _durationToDecrease = duration - delayBeforeDecrease;
        // Use a sequence to chain the increase and decrease tweens together
        Sequence sequence = DOTween.Sequence();

        // Step 2: Wait for the delayBeforeDecrease time before starting the decrease
        sequence.AppendInterval(delayBeforeDecrease);

        // Step 3: Decrease the object's scale back to its original size over the specified duration
        sequence.Append(transform.DOScale(_endSize, _durationToDecrease).SetEase(easingType))
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
