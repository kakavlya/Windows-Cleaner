using UnityEngine;

namespace WindowsCleaner.Core
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float _slowdownFactor = 0.25f;
        [SerializeField] private float _slowdownLength = 2f;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Time.timeScale = _slowdownFactor;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
            else
            {
                Time.timeScale += (1f / _slowdownLength) * Time.deltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            }
        }
    }
}