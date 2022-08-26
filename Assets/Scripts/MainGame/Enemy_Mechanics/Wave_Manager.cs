using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
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
        [HideInInspector] public int numberOfEnemies;
        [HideInInspector] public List<ObjectPooler> poolsOfEnemies = new List<ObjectPooler>();
    }
    private int _currentWave = 1;
    private ObjectPooler _poolOfEnemies;
    private Building_Script _currentBuildingScript;
    [SerializeField] private List<Wave> _waves = new List<Wave>();

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(6, 6);
    }

    void Start()
    {
        CreateEnemies();
        SpawnWave(_currentWave);    
    }

    private void CreateEnemies()
    {
        foreach (Wave wave in _waves)
        {
            foreach (Wave.Enemy enemy in wave.enemyPoolers)
            {
                wave.numberOfEnemies += enemy.amountToPool;
                enemy.objectToPool.GetComponent<Basic_Enemy_Script>().waveControllerScript = this;
                _poolOfEnemies = new ObjectPooler();
                _poolOfEnemies.poolFilling(enemy.objectToPool, enemy.amountToPool);
                wave.poolsOfEnemies.Add(_poolOfEnemies);
            }
        }
    }

    private void SpawnWave(int wave)
    {
        --wave;
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

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(10f);
        SpawnWave(_currentWave);
    }

    public void EnemyDied()
    {
        _waves[_currentWave-1].numberOfEnemies -= 1;
        if (_waves[_currentWave - 1].numberOfEnemies == 0)
        {
            ++_currentWave;
            if (_currentWave > _waves.Count)
            {
                Debug.Log("Level is finished");
            }
            else
            {
                foreach (GameObject building in GameObject.FindGameObjectsWithTag("Building"))
                {
                    _currentBuildingScript = building.GetComponent<Building_Script>();
                    if (_currentBuildingScript.IsDamaged())
                    {
                        if (_currentBuildingScript.IsRuined()) _currentBuildingScript.Heal(1);
                        building.GetComponent<SpriteRenderer>().enabled = true;
                        building.GetComponent<BoxCollider2D>().enabled = true;
                        _currentBuildingScript.ActivateBuilder();
                    }
                }
                StartCoroutine(NextWave());
            }
        }
    }
}
