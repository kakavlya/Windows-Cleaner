using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlacer : MonoBehaviour
{
    [SerializeField] private Wall _wall;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerMop;
    [SerializeField] private float _topOffset;
    [SerializeField] private float _mopOffset;

    private void OnEnable()
    {
        if(_wall != null)
        {
            _wall.WallGenerated += PlacePlayer;
        }
    }

    private void OnDisable()
    {
        if(_wall != null)
        {
            _wall.WallGenerated -= PlacePlayer;
        }
    }

    private void PlacePlayer()
    {
        if(_player != null)
        {
            Vector3 pointToPlace = new Vector3(_wall.GetTopCenterPoint().x, _wall.GetTopCenterPoint().y + _topOffset, _wall.GetTopCenterPoint().z - 1 );
            Vector3 mopPointToPlace = new Vector3(_wall.GetTopCenterPoint().x, _wall.GetTopCenterPoint().y + _topOffset + _mopOffset, _wall.GetTopCenterPoint().z - 1);

            _player.transform.position = pointToPlace;
            _playerMop.transform.position = mopPointToPlace;
        }
    }
}
