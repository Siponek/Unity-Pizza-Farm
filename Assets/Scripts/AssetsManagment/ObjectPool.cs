using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int initialSize = 10;
        public bool shouldExpand = true;
    }
    public List<Pool> poolsForType;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, Pool> poolsDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        poolsDictionary = new Dictionary<string, Pool>();

        foreach (var pool in poolsForType)
        {
            Debug.Log($"Creating new pool with tag {pool.tag}");   
            Queue<GameObject> objectPool = new();
            
            for (int i =0; i<  pool.initialSize; i++)
            {
                GameObject obj = CreateNewObject(pool.prefab);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
            poolsDictionary.Add(pool.tag, pool);
        }


    }
    private GameObject CreateNewObject(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is null");
            return null;
        }
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }



    // Call this method to get an object from the pool
    public GameObject GetObjectFromPool(string poolTag)
    {
        if (!poolDictionary.ContainsKey(poolTag) )
        {
            Debug.LogError("Pool with tag " + poolTag + " does not exist.");
            return null;
        }
        if (poolsDictionary[poolTag].shouldExpand && poolDictionary[poolTag].Count == 0)
        {
            GameObject objectToSpawn = CreateNewObject(poolsDictionary[poolTag].prefab);
            objectToSpawn.SetActive(true);
            return objectToSpawn;
        }
        else if (poolDictionary[poolTag].Count > 0)
        {
            
            GameObject objectToSpawn = poolDictionary[poolTag].Dequeue();
            objectToSpawn.SetActive(true);
            return objectToSpawn;
        
        }
        Debug.Log("No object available for prefab at pool: " + poolTag);
        return null;
    }

    // Call this method to return an object to the pool
    public void ReturnObjectToPool(string poolTag, GameObject objectToReturn)
    {
        if(!poolDictionary.ContainsKey(poolTag))
        {
            Debug.Log("Pool tag" + poolTag + "Not found in dictionary of pools");
            return;
        }
        objectToReturn.SetActive(false);
        poolDictionary[poolTag].Enqueue(objectToReturn);

    }
    public int GetPoolCount(string tag)
    {
        if (poolDictionary.TryGetValue(tag, out var queue))
        {
            return queue.Count;
        }
        return 0;
    }
}
