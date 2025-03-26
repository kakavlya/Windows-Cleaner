using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArrowHint : MonoBehaviour
{
    [SerializeField]
    private RectTransform arrowTransform; 

    [SerializeField]
    private float targetScale = 1.2f; 

    [SerializeField]
    private float duration = 0.5f; 

    [SerializeField]
    private int repeatCount = -1; // -1 for endless animation

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
            Debug.LogError("ArrowTransform не назначен!");
            return;
        }

        arrowTransform.localScale = Vector3.one;

        pulseTween = arrowTransform.DOScale(targetScale, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(repeatCount, LoopType.Yoyo)
            .SetUpdate(true); // Update using real time and not game time scale
    }

    public void StopPulseAnimation()
    {
        if (pulseTween != null && pulseTween.IsActive())
        {
            pulseTween.Kill();
            pulseTween = null;
        }
        // Возвращаем исходный масштаб
        if (arrowTransform != null)
        {
            arrowTransform.localScale = Vector3.one;
        }
    }
}