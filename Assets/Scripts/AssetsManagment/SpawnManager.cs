using UnityEngine;

/** This manages the spawns on the map and can be called to spawn enemies
 */
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How often to spawn enemies manually when pressing a button")]
    [Range(1f, 10f)]
    public float manualSpawnRate = 1f;
    [SerializeField]
    [Tooltip("How often to spawn enemies automatically")]
    [Range(1f, 10f)]
    public float autoSpawnRate = 5f;
    public float spawnRadius = 10f;
    public float spawnHeight = 1f;
    public float spawnDistance = 10f;
    public static readonly string[] enemyTypesTags = { "cow_enemy", "doe_enemy", "dog_enemy" };
    private ObjectPool objectPoolInstance;
    private float lastSpawnTime = 0f;

    void Start()
    {
        autoSpawnRate = Mathf.Clamp(autoSpawnRate, 1f, 100f);
        manualSpawnRate = Mathf.Clamp(manualSpawnRate, 1f, 100f);
        objectPoolInstance = FindFirstObjectByType<ObjectPool>();
        if (objectPoolInstance == null)
        {
            Debug.LogError("ObjectPool is null. Check SpawnManager.");
        }
    }

    void Update()
    {
        if (Time.time - lastSpawnTime >= autoSpawnRate)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
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
        if (Time.time - lastSpawnTime < manualSpawnRate)
        {
            return;
        }
        lastSpawnTime = Time.time;

        string enemyTag = GetRandomNameFromList(enemyTypesTags);
        GameObject newEnemy = objectPoolInstance.GetObjectFromPool(poolTag: enemyTag);
        if (newEnemy != null) newEnemy.transform.position = GetRandomSpawnPoint();
        else Debug.Log($"SpawnEnemy: Cannot spawn a new enemy. Object pool of avaiable {enemyTag} is empty");
    }
    private Vector3 GetRandomSpawnPoint()
    {
        float randomX = Random.Range(-spawnRadius, spawnRadius);
        float randomZ = Random.Range(-spawnHeight, spawnHeight) + spawnDistance;
        return new Vector3(randomX, 0, randomZ);
    }
}
