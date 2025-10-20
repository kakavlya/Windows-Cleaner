using UnityEngine;

namespace WindowsCleaner.Core
{
    public class TimeManager : MonoBehaviour
    {
        public float SlowdownFactor = 0.25f;
        public float SlowdownLength = 2f;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Time.timeScale = SlowdownFactor;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
            else
            {
                Time.timeScale += (1f / SlowdownLength) * Time.deltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            }
        }
    }
}