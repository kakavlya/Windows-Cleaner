using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "GameObjectsSettings", menuName = "Game/Game Objects Settings")]
[CreateAssetMenu(fileName = "GameObjectSettings", menuName = "Game/Game Object Settings")]
public class GameObjectsSettings : ScriptableObject
{
    [Header("Obstacles settings")]
    public GameObjectTypeSettings ObstaclesSettings;

    [Header("Pickables settings")]
    public GameObjectTypeSettings PickablesSettings;
}
