using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSwing : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private float _timer = 0f;
    private int _phase = 0;

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > 1f)
        {
            _phase++;
            _phase %= 4;
            _timer = 0f;
        }

        switch (_phase)
        {
            //case 0:
            //    transform.Rotate(0f, _speed * (1 - _timer), 0f);
            //    break;
            //case 1:
            //    transform.Rotate(0f, -_speed * _timer, 0f);
            //    break;
            //case 2:
            //    transform.Rotate(0f, -_speed * (1 - _timer), 0f);
            //    break;
            //case 3:
            //    transform.Rotate(0f, -_speed * _timer, 0f);
            //    break;
            case 0:
                transform.Rotate(0f, 0f, _speed * (1 - _timer));
                break;
            case 1:
                transform.Rotate(0f, 0f, -_speed * _timer);
                break;
            case 2:
                transform.Rotate(0f, 0f, -_speed * (1 - _timer));
                break;
            case 3:
                transform.Rotate(0f, 0f, _speed * _timer);
                break;
        }
    }
}
