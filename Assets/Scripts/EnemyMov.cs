using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    Transform balloonTransform; // Reference to the balloon's transform
    Vector3 initialScale;      // Store the initial scale of the sprite

    void Start()
    {
        // Find the balloon by tag
        GameObject balloon = GameObject.FindGameObjectWithTag("Balloon");

        if (balloon != null)
        {
            balloonTransform = balloon.transform;
        }
        else
        {
            Debug.LogError("Ballon not found. Make sure the balloon is tagged as 'Balloon'.");
        }

        // Store the initial scale of the enemy
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (balloonTransform == null) return;

        // Flip the sprite based on the balloon's position
        FlipSprite();

        // Move toward the balloon
        MoveTowardsBalloon();
    }

    void FlipSprite()
    {
        // Check if the balloon is to the left or right of the enemy
        if (balloonTransform.position.x < transform.position.x)
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

    void MoveTowardsBalloon()
    {
        // Calculate the direction to the balloon
        Vector2 directionToPlayer = (balloonTransform.position - transform.position).normalized;

        // Move the enemy in the calculated direction
        transform.Translate(directionToPlayer * speed * Time.deltaTime, Space.World);
    }
}

