using UnityEngine;
using UnityEngine.Serialization;

namespace WindowsCleaner.Obstacles
{
    [CreateAssetMenu(fileName = "GameObjectSettings", menuName = "Game/Game Object Settings")]
    public class GameObjectsSettings : ScriptableObject
    {
        [Header("Obstacles settings")]
        [FormerlySerializedAs("ObstaclesSettings")]
        [SerializeField] private GameObjectTypeSettings _obstaclesSettings;

        [Header("Pickables settings")]
        [FormerlySerializedAs("PickablesSettings")]
        [SerializeField] private GameObjectTypeSettings _pickablesSettings;

        public GameObjectTypeSettings ObstaclesSettings => _obstaclesSettings;
        public GameObjectTypeSettings PickablesSettings => _pickablesSettings;
    }
}
