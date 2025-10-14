using DG.Tweening;
using UnityEngine;

namespace WindowsCleaner.UI
{
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
            gameObject.SetActive(false);
        }

        public void IncreaseAndDecreaseSizeWithDelay()
        {
            gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOScale(_enlargeSize, _duration).SetEase(easingType));
            sequence.AppendInterval(delayBeforeDecrease);
            sequence.Append(transform.DOScale(_endSize, _duration).SetEase(easingType))
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }

        private void FixedUpdate()
        {
            GetComponent<Rigidbody>().AddTorque(_spinForce * Time.fixedDeltaTime * Vector3.up);
        }
    }
}