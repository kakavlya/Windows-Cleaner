using DG.Tweening;
using UnityEngine;
using WindowsCleaner.Core;

namespace WindowsCleaner.UI
{
    public class UICoinsExplosion : MonoBehaviour, IHaveDurartion
    {
        [SerializeField] private Transform _startPos;
        [SerializeField] private Transform _intermediatePos;
        [SerializeField] private Transform _endPos;
        [SerializeField] private GameObject _Canvas;
        [SerializeField] private GameObject _UICoin;
        [SerializeField] private float _minDuration = 1f;
        [SerializeField] private float _maxDuration = 2f;
        [SerializeField] private float _scaleSize = 0.5f;
        [SerializeField] private float _spread = 30f;
        [SerializeField] private int _maxCoins = 20;

        public void Start2DCoinsAnimation()
        {
            Vector3 startingPos = _startPos.position;
            Vector3 intermediatePos = _intermediatePos.position;
            Vector3 endPos = GetCenteredEndPosition();

            for (int i = 0; i < _maxCoins; i++)
            {
                GameObject coin = Instantiate(_UICoin, _Canvas.transform);

                coin.transform.position = startingPos;
                Vector3 cointIntermedPos = intermediatePos + Helpers.GetRandomPosXY(_spread);
                coin.transform.DOMove(cointIntermedPos, _minDuration)
                    .SetEase(Ease.Flash)
                    .OnComplete(() =>
                    {
                        float duration = Random.Range(_minDuration, _maxDuration);
                        coin.transform.DOScale(_scaleSize, duration);
                        coin.transform.DOMove(endPos, duration)
                        .SetEase(Ease.Flash)
                        .OnComplete(() =>
                        {
                            coin.SetActive(false);
                        });
                    });
            }
        }

        public void SetDuration(float duration)
        {
            _maxDuration = duration;
        }

        private Vector3 GetCenteredEndPosition()
        {
            RectTransform rectTransform = _endPos.GetComponent<RectTransform>();
            Vector3 screenPos = new Vector3(_endPos.position.x, _endPos.position.y, 0);
            float width = rectTransform.rect.width;
            float height = rectTransform.rect.height;

            Vector3 centeredPos = screenPos - new Vector3(width / 4, height / 4, 0);
            return centeredPos;
        }
    }
}