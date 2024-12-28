using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject ratPrefab; 

    [SerializeField]
    GameObject birdPrefab; 

    [SerializeField]
    float spawnInterval = 12f; // Time interval between spawns

    [SerializeField]
    Vector3 spawnAreaSize = new Vector3(20, 1, 20); // Size of the area (x, y, z)

    void Start()
    {
        // Start spawning enemies repeatedly
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Randomly select between Rat and Bird
        GameObject enemyToSpawn = Random.value < 0.5f ? ratPrefab : birdPrefab;

        // Generate a random position within the spawn area
        Vector3 randomPosition = GetRandomPosition();

        // Spawn the enemy
        Instantiate(enemyToSpawn, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        // Calculate a random position within the spawn area relative to the spawner
        Vector3 offset = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        return transform.position + offset; // Offset from the spawner's position
    }

    void OnDrawGizmosSelected()
    {
        // Draw the spawn area in the Scene view for visualization
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}

