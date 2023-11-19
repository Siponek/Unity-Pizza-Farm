using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float xMinBound = -15f;
    public float xMaxBound = 15f;
    public float zMinBound = -15f;
    public float zMaxBound = 15f;
    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = FindFirstObjectByType<ObjectPool>();
    }
    void Update()
    {
        // Check each boundary separately to determine where the object passed the bounds
        if (transform.position.x <= xMinBound)
        {
            DestroyAndLog("left");
        }
        else if (transform.position.x >= xMaxBound)
        {
            DestroyAndLog("right");
        }
        else if (transform.position.z <= zMinBound)
        {
            DestroyAndLog("front");
        }
        else if (transform.position.z >= zMaxBound)
        {
            DestroyAndLog("back");
        }
    }

    // Log where the object passed the bounds and destroy it
    private void DestroyAndLog(string direction)
    {
        string poolTag = gameObject.tag;
        Debug.Log($"{gameObject.name} with tag {poolTag} passed the {direction} boundary.");
        objectPool.ReturnObjectToPool(poolTag: poolTag, objectToReturn: gameObject);
    }

    void Start()
    {
    }
}
