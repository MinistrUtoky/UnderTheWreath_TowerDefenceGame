using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pooledObjects = new List<GameObject>(); 
    public GameObject objectToPool;
    public int amountToPool;

    void Start()
    {
        poolFilling(objectToPool, amountToPool);
    }

    public void poolFilling(GameObject obj, int amount)
    {
        if (objectToPool == null) objectToPool = obj;
        if (amountToPool == 0) amountToPool = amount;
        _pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(obj);
            tmp.SetActive(false);
            _pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null; 
    }
}
