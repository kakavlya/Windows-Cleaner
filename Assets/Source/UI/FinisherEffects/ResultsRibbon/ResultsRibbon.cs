using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsRibbon : MonoBehaviour
{
    //[SerializeField] private float _startSize = 0.1f;
    [SerializeField] private float _enlargeSize = 0.5f;
    [SerializeField] public float _ribbonOnScreenDuration = 3f;
    [SerializeField] private float _duration = 1.2f;
    [SerializeField] private float _starsIncreaseDuration = 0.4f;
    [SerializeField] private float delayBeforeDecrease = 0.5f;
    [SerializeField] private float _endSize = 2.2f;
    [SerializeField] private float _starEnlargeMax = 2.0f;
    [SerializeField] private float _starEnlargeMin = 1.1f;
    [SerializeField] private Ease _easingType = Ease.OutQuad;
    [SerializeField] private Ease _easingTypeStars = Ease.InOutSine;
    [SerializeField] private Ease _easingTypeStarsBack = Ease.InOutBounce;
    [SerializeField] private GameObject _ribbon;
    [SerializeField] private Transform[] _starsTransforms;

    private Transform _ribbonTransform;
    private bool _showRibbon;

    private void DisplayRibbonSequence()
    {
        _showRibbon = true;
        _ribbon.SetActive(true);
        _ribbonTransform = _ribbon.transform;
        _ribbonTransform.localScale = new Vector3(0, 0, 0);
        DisplayRibbonSequence(_ribbonTransform);
        // TODO create timer, that will stop the method after certain amount of time and hide all objects
        // Until then scale the stars
        StartCoroutine(StartHideSequence());
    }

    public void StartRibbonSequence()
    {
        StartRibbonSequenceAfterDelay(0);
    }

    public void StartRibbonSequenceAfterDelay(float delay)
    {
        StartCoroutine(RibbonSequence(delay));
    }

    private IEnumerator RibbonSequence(float delay)
    {
        yield return new WaitForSeconds(delay);

        DisplayRibbonSequence();
    }

    public void DisplayRibbonSequence(Transform transform)
    {
        StartCoroutine(StartHideSequence());

        //gameObject.SetActive(true);
        // Use a sequence to chain the increase and decrease tweens together
        Sequence sequence = DOTween.Sequence();

        // Step 1: Increase the object's scale to the targetScale over the specified duration
        sequence.Append(transform.DOScale(_enlargeSize, _duration).SetEase(_easingType));

        // Step 2: Wait for the delayBeforeDecrease time before starting the decrease
        sequence.AppendInterval(delayBeforeDecrease);

        // Step 3: Decrease the object's scale back to its original size over the specified duration
        sequence.Append(transform.DOScale(_endSize, _duration).SetEase(_easingType))
            .OnComplete(() =>
            {
                // start stars method
                //_ribbon.SetActive(false);
                ScaleStars();
            });
    }

    private void ScaleStars()
    {
        if(!_showRibbon ) { return; }

        foreach(var star in _starsTransforms)
        {
            float _scaleTo = Random.Range(_starEnlargeMin, _starEnlargeMax);
            star.DOScale(_scaleTo, _starsIncreaseDuration)
                .SetEase(_easingTypeStars)
                .OnComplete(() =>
                {
                    star.DOScale(1.0f, _starsIncreaseDuration)
                    .SetEase(_easingTypeStars)
                    .SetDelay(1.0f)
                    .OnComplete(ScaleStars);
                });
        }
    }

    private IEnumerator StartHideSequence()
    {

        yield return new WaitForSeconds(_ribbonOnScreenDuration);

        _showRibbon = false;
        _ribbon.SetActive(false);
    }

    public void SetDuration(float duration)
    {
        _ribbonOnScreenDuration = duration;
    }
}
