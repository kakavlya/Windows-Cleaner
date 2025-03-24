using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBootstrapper : MonoBehaviour
{
    [SerializeField] private GameObject _audioPrefab;

    private void Awake()
    {
        if(Audio.Instance == null)
        {
            Instantiate(_audioPrefab);
        }
    }
}
