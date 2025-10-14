using System.Collections.Generic;
using UnityEngine;

namespace WindowsCleaner.UI
{
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
            for (int i = 0; i < coinPool.Count; i++)
            {
                if (!coinPool[i].activeInHierarchy)
                {
                    return coinPool[i];
                }
            }

            GameObject newCoin = Instantiate(_coinPrefab);
            newCoin.SetActive(false);
            coinPool.Add(newCoin);
            return newCoin;
        }
    }
}