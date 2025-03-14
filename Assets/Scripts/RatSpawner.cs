﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject ratPrefab; // Only using ratPrefab for spawning

    [SerializeField]
    float spawnInterval = 12f; // Time interval between spawns

    [SerializeField]
    Vector2 spawnAreaSize = new Vector2(20, 20); // Size of the area (x, y) for 2D

    void Start()
    {
        // Start spawning enemies repeatedly
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Always spawn the ratPrefab
        GameObject enemyToSpawn = ratPrefab;

        // Generate a random position within the spawn area
        Vector2 randomPosition = GetRandomPosition();

        // Spawn the enemy
        Instantiate(enemyToSpawn, randomPosition, Quaternion.identity);
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

    void OnDrawGizmosSelected()
    {
        // Draw the spawn area in the Scene view for visualization
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0));
    }
}
