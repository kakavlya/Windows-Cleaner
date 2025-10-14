using UnityEngine;
using WindowsCleaner.GameProgressNs;
using WindowsCleaner.Obstacles;

namespace WindowsCleaner.Environment
{
    public class EnvironmentGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] _environments;
        [SerializeField] private Vector3 _position;

        private void Start()
        {
            if (PersistentData.EnvironmentPrefabIndex.HasValue && LevelController.Instance.IsRestartingLevel)
            {
                int index = PersistentData.EnvironmentPrefabIndex.Value;
                GameObject env = Instantiate(_environments[index], _position, Quaternion.identity);
                env.transform.SetParent(transform);
            }
            else
            {
                GenerateRandomEnvironment();
            }
        }

        private void GenerateRandomEnvironment()
        {
            int envToGenerate = UnityEngine.Random.Range(0, _environments.Length);
            GameObject newEnv = Instantiate(_environments[envToGenerate], _position, Quaternion.identity);
            newEnv.transform.SetParent(transform);

            PersistentData.EnvironmentPrefabIndex = envToGenerate;
        }
    }
}