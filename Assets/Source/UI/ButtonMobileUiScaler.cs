using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ButtonMobileUiScaler : MonoBehaviour
{
    [SerializeField] private float _scaleMultiplier = 2f;
    [SerializeField] private bool _adjustPosition = true;

    void Start()
    {
        if (Application.isMobilePlatform)
        {
            RectTransform rt = GetComponent<RectTransform>();
            Vector2 originalSize = rt.sizeDelta;

            rt.sizeDelta *= _scaleMultiplier;

            Vector2 deltaSize = rt.sizeDelta - originalSize;

            if (_adjustPosition)
            {
                Vector2 positionOffset = new Vector2(
                    (rt.anchorMin.x + rt.anchorMax.x) > 1f ? deltaSize.x * 0.5f : (rt.anchorMin.x + rt.anchorMax.x) < 1f ? -deltaSize.x * 0.5f : 0f,
                    (rt.anchorMin.y + rt.anchorMax.y) > 1f ? deltaSize.y * 0.5f : (rt.anchorMin.y + rt.anchorMax.y) < 1f ? -deltaSize.y * 0.5f : 0f
                );

                rt.anchoredPosition += positionOffset;
            }
        }
    }
}
