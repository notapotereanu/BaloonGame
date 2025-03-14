﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    float maxShootDistance = 500f; // Maximum range of the hitscan

    [SerializeField]
    LayerMask collisionMask; // Layer mask for obstacles that block the shot

    [SerializeField]
    AudioSource shootAudioSource; // Reference to the AudioSource component for the shooting sound

    [SerializeField]
    float shootCooldown = 1f; // Time (in seconds) between shots

    [SerializeField]
    private LineRenderer lineRenderer; // Reference to the LineRenderer component

    [SerializeField]
    private float rayDuration = 0.5f; // Duration the ray stays visible

    float lastShootTime = -1f; // Tracks the last time the player shot

    private GameLogic gameLogic; // Reference to the GameLogic script

    void Start()
    {
        // Find the GameLogic script in the scene
        gameLogic = FindObjectOfType<GameLogic>();
        if (gameLogic == null)
        {
            Debug.LogError("GameLogic script not found in the scene!");
        }

        // Ensure the LineRenderer is initialized
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set up the LineRenderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;

        // Hide the LineRenderer initially
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShootTime + shootCooldown) // Left click and cooldown check
        {
            Shoot();
            lastShootTime = Time.time; // Update the last shoot time
        }
    }

    void Shoot()
    {
        // Play the shooting sound
        if (shootAudioSource != null)
        {
            shootAudioSource.Play();
        }

        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to a point in the world
        Ray rayFromCamera = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.forward, Vector3.zero); // Adjust for 2D plane

        if (groundPlane.Raycast(rayFromCamera, out float distanceToGround))
        {
            Vector3 targetPoint = rayFromCamera.GetPoint(distanceToGround);

            // Create a ray from the player's position to the target point
            Vector3 direction = (targetPoint - transform.position).normalized;
            Ray rayFromPlayer = new Ray(transform.position, direction);

            // Draw the ray
            DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * maxShootDistance);

            // Check for a direct hit
            if (Physics2D.Raycast(transform.position, direction, maxShootDistance, collisionMask))
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, maxShootDistance, collisionMask);
                GameObject hitObject = hitInfo.collider.gameObject;

                // If the hit object is tagged as "Enemy", destroy it and update the score
                if (hitObject.CompareTag("Enemy"))
                {
                    Destroy(hitObject);
                    gameLogic.IncrementScore(); // Notify GameLogic to increment the score
                    return;
                }
            }

            // Check for enemies along the shooting path
            CheckLineToEnemy(rayFromPlayer);
        }
    }

    void CheckLineToEnemy(Ray ray)
    {
        // Get all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector3 enemyPosition = enemy.transform.position;

            // Calculate the direction and distance to the enemy
            Vector3 directionToEnemy = (enemyPosition - ray.origin).normalized;
            float distanceToEnemy = Vector3.Distance(ray.origin, enemyPosition);

            // Ensure the enemy lies approximately along the shooting ray
            if (Vector3.Dot(ray.direction, directionToEnemy) < 0.99f) // Small tolerance
                continue;

            // Check if there are no obstacles between the player and the enemy
            RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, directionToEnemy, distanceToEnemy, collisionMask);
            if (hitInfo.collider != null && hitInfo.collider.gameObject != enemy)
            {
                // If there's an obstacle between the player and the enemy, skip it
                continue;
            }

            // Destroy the enemy if it's hit and not out of sight
            Destroy(enemy);
            gameLogic.IncrementScore(); // Notify GameLogic to increment the score
        }
    }

    void DrawRay(Vector3 start, Vector3 end)
    {
        // Enable the LineRenderer
        lineRenderer.enabled = true;

        // Set the positions of the LineRenderer
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start + end);

        // Start a coroutine to hide the ray after a short duration
        StartCoroutine(HideRayAfterDelay());
    }

    IEnumerator HideRayAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(rayDuration);

        // Disable the LineRenderer
        lineRenderer.enabled = false;
    }
}


