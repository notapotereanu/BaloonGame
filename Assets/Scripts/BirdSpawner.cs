using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject birdPrefab; // Only using birdPrefab for spawning

    [SerializeField]
    float spawnInterval = 12f; // Time interval between spawns

    [SerializeField]
    Vector2 spawnAreaSize = new Vector2(20, 20); // Size of the area (x, y) for 2D

    [SerializeField]
    private int enemyHealth = 3; // Default health for spawned enemies

    void Start()
    {
        // Start spawning enemies repeatedly
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Always spawn the birdPrefab
        GameObject enemyToSpawn = birdPrefab;

        // Generate a random position within the spawn area
        Vector2 randomPosition = GetRandomPosition();

        // Spawn the enemy
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, randomPosition, Quaternion.identity);

        // Set the health of the spawned enemy
        SetEnemyHealth(spawnedEnemy, enemyHealth);
    }

    Vector2 GetRandomPosition()
    {
        // Calculate a random position within the spawn area relative to the spawner
        Vector2 offset = new Vector2(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        return (Vector2)transform.position + offset; // Offset from the spawner's position
    }

    // Method to set the health of the spawned enemy
    private void SetEnemyHealth(GameObject enemy, int health)
    {
        // Get the EnemyHealth component from the spawned enemy
        EnemyHealth enemyHealthComponent = enemy.GetComponent<EnemyHealth>();
        if (enemyHealthComponent != null)
        {
            // Set the health of the enemy
            enemyHealthComponent.SetHealth(health);
        }
        else
        {
            Debug.LogWarning("EnemyHealth component not found on the spawned enemy!");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the spawn area in the Scene view for visualization
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0));
    }
}
