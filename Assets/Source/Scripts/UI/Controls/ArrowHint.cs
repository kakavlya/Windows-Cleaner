using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace WindowsCleaner.UI
{
    public class ArrowHint : MonoBehaviour
    {
        [FormerlySerializedAs("arrowTransform")]
        [SerializeField]
        private RectTransform _arrowTransform;

        [SerializeField]
        private float _targetScale = 1.2f;

        [SerializeField]
        private float _duration = 0.5f;

        [SerializeField]
        private int _repeatCount = -1;

        [FormerlySerializedAs("playOnEnable")]
        [SerializeField]
        private bool _playOnEnable = true;

        private Tween _pulseTween;

        private void OnEnable()
        {
            if (_playOnEnable)
            {
                StartPulseAnimation();
            }
        }

        private void OnDisable()
        {
            StopPulseAnimation();
        }

        public void StartPulseAnimation()
        {
            if (_arrowTransform == null)
            {
                return;
            }

            _arrowTransform.localScale = Vector3.one;

            _pulseTween = _arrowTransform.DOScale(_targetScale, _duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(_repeatCount, LoopType.Yoyo)
                .SetUpdate(true);
        }

        public void StopPulseAnimation()
        {
            if (_pulseTween != null && _pulseTween.IsActive())
            {
                _pulseTween.Kill();
                _pulseTween = null;
            }

            if (_arrowTransform != null)
            {
                _arrowTransform.localScale = Vector3.one;
            }
        }
    }
}