using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrow : MonoBehaviour
{
    private float _spinForce;

    public void SetSpinForce(float spinForce)
    {
        _spinForce = spinForce;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddTorque(Vector3.up * _spinForce * Time.fixedDeltaTime);
    }
}
