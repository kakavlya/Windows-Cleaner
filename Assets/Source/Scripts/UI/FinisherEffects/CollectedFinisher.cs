using System.Collections;
using UnityEngine;

namespace WindowsCleaner.UI
{
    public class CollectedFinisher : MonoBehaviour
    {
        [SerializeField] private float _waitingTimeCoroutineStarting = 1.5f;
        [SerializeField] private float _duration = 3.0f;
        [SerializeField] private float _ribbonDuration = 7.0f;

        private CoinsExplosion _coinsExplosion;
        private DustExplosion _dustExplosion;
        private UICoinsExplosion _uiCoinsExplosion;
        private ResultsRibbon _resultsRibbon;
        private CoinsRewardAppearance _coinsRewardAppearance;

        private void Start()
        {
            _coinsExplosion = GetComponent<CoinsExplosion>();
            _dustExplosion = GetComponent<DustExplosion>();
            _uiCoinsExplosion = GetComponent<UICoinsExplosion>();
            _resultsRibbon = GetComponent<ResultsRibbon>();
            _coinsRewardAppearance = GetComponent<CoinsRewardAppearance>();

            _coinsExplosion.SetDuration(_duration);
            _dustExplosion.SetDuration(_duration);
            _uiCoinsExplosion.SetDuration(_duration);
            _resultsRibbon.SetDuration(_ribbonDuration);
        }

        public void StartFinishingSequence()
        {
            StartCoroutine(AnimateFromPrefabRoutine());
        }

        private IEnumerator AnimateFromPrefabRoutine()
        {
            yield return new WaitForSeconds(_waitingTimeCoroutineStarting);

            _coinsExplosion.StartExplosion();
            _dustExplosion.PlayEffect();
            _uiCoinsExplosion.Start2DCoinsAnimation();
            _resultsRibbon.StartRibbonSequenceAfterDelay(_duration);
            _coinsRewardAppearance.StartRewardsSequence();
        }
    }
}