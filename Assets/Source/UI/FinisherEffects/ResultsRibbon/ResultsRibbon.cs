using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private float _starPauseBeforeAppearance = 0.2f;
    [SerializeField] private Ease _easingType = Ease.OutQuad;
    [SerializeField] private AnimationCurve _easingCurve;
    [SerializeField] private Ease _easingTypeStars = Ease.InOutSine;
    [SerializeField] private Ease _easingTypeStarsBack = Ease.InOutBounce;
    [SerializeField] private GameObject _ribbon;
    [SerializeField] private Transform[] _starsTransforms;
    [SerializeField] private TMP_Text _scoresText;
    [SerializeField] private Scores _scores;

    private Transform _ribbonTransform;
    private bool _showRibbon;

    private void DisplayRibbonSequence()
    {
        _showRibbon = true;
        _ribbon.SetActive(true);
        _ribbonTransform = _ribbon.transform;
        _ribbonTransform.localScale = new Vector3(0, 0, 0);
        DisplayRibbonSequence(_ribbonTransform);
        StartCoroutine(StartHideSequence());
        ShowScore();
    }

    private void ShowScore()
    {
        float scoreValue = _scores.GetCurrentScore();

        _scoresText.SetText(scoreValue.ToString("#.#") + "%");
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

        Sequence sequence = DOTween.Sequence();

        //sequence.Append(transform.DOScale(_enlargeSize, _duration).SetEase(_easingType));

        //sequence.AppendInterval(delayBeforeDecrease);

        sequence.Append(transform.DOScale(_endSize, _duration).SetEase(_easingCurve))
            .OnComplete(() =>
            {
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
                    .SetDelay(_starPauseBeforeAppearance)
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
