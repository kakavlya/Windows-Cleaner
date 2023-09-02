using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustResizing : MonoBehaviour
{

    [SerializeField] private float _startSize = 1f;
    [SerializeField] private float _enlargeSize = 2.0f;
    [SerializeField] private float _duration = 1.2f;
    [SerializeField] private float delayBeforeDecrease = 0.5f;
    [SerializeField] private float _endSize = 0.2f;
    [SerializeField] private float _spinForce = 100f;
    [SerializeField] private Ease easingType = Ease.OutQuad;

    private void Start()
    {
        // Start the size increase when the script is initialized (you can trigger this in other ways too)
        //IncreaseAndDecreaseSizeWithDelay();
        gameObject.SetActive(false);
    }

    public void IncreaseAndDecreaseSizeWithDelay()
    {
        gameObject.SetActive(true);
        // Use a sequence to chain the increase and decrease tweens together
        Sequence sequence = DOTween.Sequence();

        // Step 1: Increase the object's scale to the targetScale over the specified duration
        sequence.Append(transform.DOScale(_enlargeSize, _duration).SetEase(easingType));

        // Step 2: Wait for the delayBeforeDecrease time before starting the decrease
        sequence.AppendInterval(delayBeforeDecrease);

        // Step 3: Decrease the object's scale back to its original size over the specified duration
        sequence.Append(transform.DOScale(_endSize, _duration).SetEase(easingType))
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddTorque(Vector3.up * _spinForce * Time.fixedDeltaTime);
    }
}
