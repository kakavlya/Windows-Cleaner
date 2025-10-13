using System.Collections.Generic;
using UnityEngine;

public class CoinsPool : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _poolSize = 10;

    private List<GameObject> coinPool;

    private void Start()
    {

        coinPool = new List<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject coin = Instantiate(_coinPrefab);
            coin.SetActive(false);
            coinPool.Add(coin);
        }
    }

    public GameObject GetPooledCoin()
    {
        // Find an inactive coin in the pool and return it
        for (int i = 0; i < coinPool.Count; i++)
        {
            if (!coinPool[i].activeInHierarchy)
            {
                return coinPool[i];
            }
        }

        // If no inactive coins are found (pool is empty), create a new one
        GameObject newCoin = Instantiate(_coinPrefab);
        newCoin.SetActive(false);
        coinPool.Add(newCoin);
        return newCoin;
    }
}