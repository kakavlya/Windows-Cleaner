using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private MopController _mopController;

    public event UnityAction GameOver;
    public event UnityAction WonLevel;
    public event UnityAction IncrementScore;

    public void Die()
    {
        if(GameOver != null)
        {
            GameOver.Invoke();
        }
    }

    public void EndLevel()
    {
        WonLevel?.Invoke();
        _mopController.Stop();
    }

    public void BrickHit()
    {
        IncrementScore?.Invoke();
    }
}
