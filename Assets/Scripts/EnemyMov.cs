using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;


    Transform playerTransform; // Reference to the player's transform
    Vector3 initialScale;      // Store the initial scale of the sprite

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

        // Store the initial scale of the enemy
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Flip the sprite based on the player's position
        FlipSprite();

        // Move toward the player
        MoveTowardsPlayer();
    }

    void FlipSprite()
    {
        // Check if the player is to the left or right of the enemy
        if (playerTransform.position.x < transform.position.x)
        {
            // Flip the sprite to face left
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
        else
        {
            // Flip the sprite to face right
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Move the enemy in the calculated direction
        transform.Translate(directionToPlayer * speed * Time.deltaTime, Space.World);
    }
}

