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
    [SerializeField] private List<Enemy> _enemyPoolers = new List<Enemy>();
    private List<ObjectPooler> _pools = new List<ObjectPooler>();
    private ObjectPooler _pool;

    public void CreateEnemies()
    {
        MakeRandomWave(1, 2);
        foreach (Enemy enemy in _enemyPoolers)
        {
            _pool = new ObjectPooler();
            _pool.poolFilling(enemy.objectToPool, enemy.amountToPool);
            _pools.Add(_pool);
        }
    }

    public void SpawnAll()
    {
        foreach (ObjectPooler pool in _pools) {
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

    private void MakeRandomWave(int from, int to)
    {
        foreach (Enemy enemy in _enemyPoolers)
        {
            enemy.amountToPool = Random.Range(from, to);
        }
    }
}
