using Unity.VisualScripting;
using UnityEngine;

/** 
 * This manages the spawns on the map and can be called to spawn enemies
 */
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public int maxEnemies = 10;
    public float spawnRate = 1f;
    public float spawnTimer = 0f;
    public float spawnRadius = 10f;
    public float spawnHeight = 1f;
    public float spawnDistance = 10f;
    public Vector3[] spawnPoints = { 
    new Vector3(0, 0, 0),
    new Vector3(0, 0, 0),
    new Vector3(0, 0, 0),
    };
    public static readonly string[] enemyTypesTags = { "cow_enemy", "doe_enemy", "dog_enemy" };
    private ObjectPool objectPoolInstance;
    private float lastSpawnTime = 0f;

    void Start()
    {
        objectPoolInstance = FindFirstObjectByType<ObjectPool>();
        if (objectPoolInstance == null)
        {
            Debug.LogError("ObjectPool is null. Check SpawnManager.");
        }
    }

    void Update()
    {
    }
    public void TriggerSpawnEnemy()
    {
        SpawnEnemy();

    }
    private string GetRandomNameFromList(string[] enemiesArray)
    {
        return enemiesArray[Random.Range(0, enemiesArray.Length)];
    }

    private void SpawnEnemy()
    {
        // Break if the spawn rate is too high
        if (Time.time - lastSpawnTime < spawnRate)
        {
            return;
        }
        lastSpawnTime = Time.time;
        string enemyTag = GetRandomNameFromList(enemyTypesTags);
        GameObject newEnemy = objectPoolInstance.GetObjectFromPool(poolTag: enemyTag);
        if (newEnemy != null) newEnemy.transform.position = GetRandomSpawnPoint();
        else Debug.Log($"SpawnEnemy: Cannot spawn a new enemy. Object pool of {enemyTag} is empty");
    }
    private Vector3 GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

}
