using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float xMin = -15f;
    public float xMax = 15f;
    public float zMin = -15f;
    public float zMax = 15f;
    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        // Get the bounds of the play area
        //xMin = -GameManager.Instance.playAreaSize.x / 2;
        //xMax = GameManager.Instance.playAreaSize.x / 2;
        //zMin = -GameManager.Instance.playAreaSize.y / 2;
        //zMax = GameManager.Instance.playAreaSize.y / 2;
    }
    void Update()
    {
        // Check each boundary separately to determine where the object passed the bounds
        if (transform.position.x <= xMin)
        {
            DestroyAndLog("left");
        }
        else if (transform.position.x >= xMax)
        {
            DestroyAndLog("right");
        }
        else if (transform.position.z <= zMin)
        {
            DestroyAndLog("front");
        }
        else if (transform.position.z >= zMax)
        {
            DestroyAndLog("back");
        }
    }

    // Log where the object passed the bounds and destroy it
    private void DestroyAndLog(string direction)
    {
        Debug.Log($"{gameObject.name} passed the {direction} boundary.");
        objectPool.ReturnObjectToPool(gameObject);
        //Destroy(gameObject);
    }

    void Start()
    {
    }
}
