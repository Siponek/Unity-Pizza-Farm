using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    [SerializeField]
    public int maxPoolSize = 10;

    [SerializeField]
    public int initialPoolSize = 10;

    // The prefab that the pool will manage
    [SerializeField]
    private GameObject objectPrefab;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    void Start()
    {
        // Optionally pre-populate the pool
        // for example, start with 10 objects
         InitializePool(10);
    }

    // Call this method to add objects to the pool
    public void InitializePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObj = Instantiate(objectPrefab);
            newObj.SetActive(false);
            availableObjects.Enqueue(newObj);
            newObj.transform.SetParent(transform);
        }
    }

    // Call this method to get an object from the pool
    public GameObject GetObjectFromPool()
    {
        // if there are avaialable objects in the pool add one to the scene
        if (availableObjects.Count > 0)
        {
            GameObject obj = availableObjects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        if (transform.childCount < maxPoolSize)
        {
            if (transform.childCount == maxPoolSize) Debug.LogWarning("Object pool is full.");
            return AddObjectToPool();
        }
        return null;
    }

    // Call this method to return an object to the pool
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        availableObjects.Enqueue(obj);
        obj.transform.SetParent(transform);
    }

    private GameObject AddObjectToPool()
    {
        GameObject newObj = Instantiate(objectPrefab);
        newObj.SetActive(false);
        availableObjects.Enqueue(newObj);
        newObj.transform.SetParent(transform);
        return newObj;
    }
}
