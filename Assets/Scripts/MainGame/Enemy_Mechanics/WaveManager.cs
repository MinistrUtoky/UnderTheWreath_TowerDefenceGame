using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    private class Wave
    {
        [System.Serializable]
        public class Enemy
        {
            public GameObject objectToPool;
            public int amountToPool;
        }
        public List<Enemy> enemyPoolers = new List<Enemy>();
        [HideInInspector] public bool isOver=false;
        [HideInInspector] public List<ObjectPooler> poolsOfEnemies = new List<ObjectPooler>();
    }
    private ObjectPooler _poolOfEnemies;
    [SerializeField] private List<Wave> _waves = new List<Wave>();

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(6, 6);
    }

    void Start()
    {
        CreateEnemies();
        SpawnAll(0);
    }

    private void CreateEnemies()
    {
        foreach (Wave wave in _waves)
        {
            foreach (Wave.Enemy enemy in wave.enemyPoolers)
            {
                _poolOfEnemies = new ObjectPooler();
                _poolOfEnemies.poolFilling(enemy.objectToPool, enemy.amountToPool);
                wave.poolsOfEnemies.Add(_poolOfEnemies);
            }
        }
    }

    private void SpawnAll(int wave)
    {
        if (_waves.Count > wave) 
            foreach (ObjectPooler pool in _waves[wave].poolsOfEnemies)
            {
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
