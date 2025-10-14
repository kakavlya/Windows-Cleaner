using UnityEngine;

namespace WindowsCleaner.UI
{
    public class CoinThrow : MonoBehaviour
    {
        private float _spinForce;

        public void SetSpinForce(float spinForce)
        {
            _spinForce = spinForce;
        }

        private void FixedUpdate()
        {
            GetComponent<Rigidbody>().AddTorque(_spinForce * Time.fixedDeltaTime * Vector3.up);
        }
    }
}