using Cinemachine;
using UnityEngine;

namespace WindowsCleaner.Misc
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera1;
        [SerializeField] private CinemachineVirtualCamera _camera2;

        [SerializeField] private int _lowPriority = 10;
        [SerializeField] private int _highPriority = 50;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_camera1.Priority >= _camera2.Priority)
                {
                    ChangeCameraPriority(_camera2, _camera1);
                }
                else
                {
                    ChangeCameraPriority(_camera1, _camera2);
                }
            }
        }

        private void ChangeCameraPriority(CinemachineVirtualCamera highPriorityCam, CinemachineVirtualCamera lowPriorityCam)
        {
            highPriorityCam.Priority = _highPriority;
            lowPriorityCam.Priority = _lowPriority;
        }
    }
}