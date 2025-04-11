using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BrickLightweight : MonoBehaviour, IBrick
{
    [SerializeField] private GameObject _debrisPrefab;
    [SerializeField] private bool _usePooling = false;
    private Collider _collider;
    private bool _isBreaking;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isBreaking || !other.GetComponent<PlayerCollisionHandler>()) return;
        Break();
    }

    public void Break()
    {
        _isBreaking = true;
        _collider.enabled = false;

        GameObject debris = Instantiate(_debrisPrefab, transform.position, transform.rotation);
        debris.transform.localScale = transform.localScale;
        debris.transform.SetParent(null);

        var controller = debris.GetComponent<BrickDebrisController>();
        if (controller != null)
            controller.Launch();

        if (_usePooling)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }
}