using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    [SerializeField]
    float speed = 5f; 

    [SerializeField]
    float rotationSpeed = 20f; 

    Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        // Find the player by tag 
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player is tagged as 'Player'.");
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Rotate toward the player
        RotateTowardsPlayer();

        // Move forward in the enemy's local forward direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void RotateTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Calculate the rotation required to face the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Smoothly rotate towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

