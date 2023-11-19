using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    private ObjectPool objectPoolInstance;
    // Start is called before the first frame update
    void Start()
    {
        objectPoolInstance = FindFirstObjectByType<ObjectPool>();
        if (objectPoolInstance == null)
        {
            Debug.LogError("ObjectPool is null. Check DetectCollisions.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} collided with {other.gameObject.name}");
        string mainObjectTag = gameObject.tag;
        //string otherObjectTag = other.gameObject.tag;
        objectPoolInstance.ReturnObjectToPool(poolTag: mainObjectTag, objectToReturn: gameObject);

    }
}
