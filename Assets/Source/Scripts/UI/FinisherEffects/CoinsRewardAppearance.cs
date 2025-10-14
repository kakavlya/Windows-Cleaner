using DG.Tweening;
using UnityEngine;

namespace WindowsCleaner.UI
{
    public class CoinsRewardAppearance : MonoBehaviour
    {
        [SerializeField] private GameObject _coinsReward;
        [SerializeField] private float _enlargeSize = 0.5f;
        [SerializeField] private float _duration = 1.2f;
        [SerializeField] private float delayBeforeDecrease = 0.5f;
        [SerializeField] private float _endSize = 2.2f;
        [SerializeField] private Ease _easingType = Ease.OutQuad;
        private Transform _coinsRewardTransform;

        public void StartRewardsSequence()
        {
            _coinsReward.SetActive(true);

            _coinsRewardTransform = _coinsReward.transform;
            _coinsRewardTransform.localScale = new Vector3(0, 0, 0);

            DisplayRewardsSequence(_coinsRewardTransform);
            TriggerRewardCount(_coinsReward.GetComponent<RewardObj>());
        }

        private void TriggerRewardCount(RewardObj coinsReward) =>
            coinsReward.TriggerRewardCount(_duration + 1);

        private void DisplayRewardsSequence(Transform _coinsRewardTransform)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_coinsRewardTransform.DOScale(_enlargeSize, _duration).SetEase(_easingType));

            sequence.AppendInterval(delayBeforeDecrease);

            sequence.Append(_coinsRewardTransform.DOScale(_endSize, _duration).SetEase(_easingType));
        }
    }
}