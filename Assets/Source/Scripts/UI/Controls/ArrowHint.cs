using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArrowHint : MonoBehaviour
{
    [SerializeField]
    private RectTransform arrowTransform; 

    [SerializeField]
    private float _targetScale = 1.2f; 

    [SerializeField]
    private float _duration = 0.5f; 

    [SerializeField]
    private int _repeatCount = -1;

    [SerializeField]
    private bool playOnEnable = true; 

    private Tween pulseTween;

    void OnEnable()
    {
        if (playOnEnable)
        {
            StartPulseAnimation();
        }
    }

    void OnDisable()
    {
        StopPulseAnimation();
    }

    public void StartPulseAnimation()
    {
        if (arrowTransform == null)
        {
            return;
        }

        arrowTransform.localScale = Vector3.one;

        pulseTween = arrowTransform.DOScale(_targetScale, _duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(_repeatCount, LoopType.Yoyo)
            .SetUpdate(true);
    }

    public void StopPulseAnimation()
    {
        if (pulseTween != null && pulseTween.IsActive())
        {
            pulseTween.Kill();
            pulseTween = null;
        }

        if (arrowTransform != null)
        {
            arrowTransform.localScale = Vector3.one;
        }
    }
}