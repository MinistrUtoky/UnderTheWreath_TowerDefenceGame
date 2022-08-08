using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawn_Enemies : MonoBehaviour
{
    [System.Serializable]
    private class Enemy {
        public GameObject objectToPool;
        public int amountToPool;
    }
    [SerializeField] private int _numberOfWaves = 1;
    [SerializeField] private List<Enemy> _enemyPoolers = new List<Enemy>();
    private List<ObjectPooler> pools = new List<ObjectPooler>();
    private ObjectPooler pool;

    void Start()
    {
        foreach (Enemy enemy in _enemyPoolers)
        {
            pool = new ObjectPooler();
            pool.poolFilling(enemy.objectToPool, enemy.amountToPool);
            pools.Add(pool);
        }
        SpawnAll();
    }

    private void SpawnAll()
    {
        foreach (var pool in pools) {
            while (true)
            {
                GameObject enemyPrefab = pool.GetPooledObject();
                if (enemyPrefab != null)
                {
                    enemyPrefab.transform.position = transform.position;
                    enemyPrefab.transform.rotation = transform.rotation;
                    enemyPrefab.SetActive(true);
                }
                else break;
            }
        }
    }
}
