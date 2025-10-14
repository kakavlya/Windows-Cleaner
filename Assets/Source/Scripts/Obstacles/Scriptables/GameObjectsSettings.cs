using UnityEngine;

namespace WindowsCleaner.Obstacles
{
    [CreateAssetMenu(fileName = "GameObjectSettings", menuName = "Game/Game Object Settings")]
    public class GameObjectsSettings : ScriptableObject
    {
        [Header("Obstacles settings")]
        public GameObjectTypeSettings ObstaclesSettings;

        [Header("Pickables settings")]
        public GameObjectTypeSettings PickablesSettings;
    }
}