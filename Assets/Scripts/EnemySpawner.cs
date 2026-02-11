using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public GameManager gameManager; // Drag GameManager here

    [Header("Spawn Rate Scaling")]
    public float startSpawnRate = 2.0f; 
    public float endSpawnRate = 0.5f;   // Faster spawning
    
    private float nextSpawnTime;

    void Update()
    {
        // Calculate the current spawn rate based on game progress
        float currentRate = Mathf.Lerp(startSpawnRate, endSpawnRate, gameManager.DifficultyPercent);

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + currentRate;
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;
        Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * 8f;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}