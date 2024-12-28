using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    float maxShootDistance = 500f; // Maximum range of the hitscan

    [SerializeField]
    LayerMask collisionMask; // Layer mask for obstacles that block the shot

    [SerializeField]
    Transform playerTransform; // Reference to the player's transform 

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to a point in the world
        Ray rayFromCamera = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(rayFromCamera, out float distanceToGround))
        {
            Vector3 targetPoint = rayFromCamera.GetPoint(distanceToGround);

            // Create a ray from the player's position to the target point
            Vector3 direction = (targetPoint - playerTransform.position).normalized;
            Ray rayFromPlayer = new Ray(playerTransform.position, direction);

            // Check for a direct hit
            if (Physics.Raycast(rayFromPlayer, out RaycastHit hitInfo, maxShootDistance, collisionMask))
            {
                GameObject hitObject = hitInfo.collider.gameObject;

                // If the hit object is tagged as "Enemy", destroy it
                if (hitObject.CompareTag("Enemy"))
                {
                    Destroy(hitObject);
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
            if (Physics.Raycast(ray.origin, directionToEnemy, out RaycastHit hitInfo, distanceToEnemy, collisionMask))
            {
                // If there's an obstacle between the player and the enemy, skip it
                if (hitInfo.collider.gameObject != enemy)
                    continue;
            }

            // Destroy the enemy if it's hit and not out of sight
            Destroy(enemy);
        }
    }
}



