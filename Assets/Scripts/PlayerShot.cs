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
    AudioSource shootAudioSource; // Reference to the AudioSource component for the shooting sound

    [SerializeField]
    float shootCooldown = 1f; // Time (in seconds) between shots

    [SerializeField]
    private int damage = 1; // Damage dealt per shot

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

            // Check for a direct hit
            if (Physics2D.Raycast(transform.position, direction, maxShootDistance, collisionMask))
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, maxShootDistance, collisionMask);
                GameObject hitObject = hitInfo.collider.gameObject;

                // If the hit object is tagged as "Enemy", deal damage
                if (hitObject.CompareTag("Enemy"))
                {
                    DealDamage(hitObject);
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

            // Deal damage to the enemy
            DealDamage(enemy);
        }
    }

    // Method to deal damage to an enemy
    private void DealDamage(GameObject enemy)
    {
        // Get the EnemyHealth component from the enemy
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage); // Deal damage to the enemy

            // Check if the enemy is destroyed
            if (enemyHealth.IsDead())
            {
                gameLogic.IncrementScore(); // Notify GameLogic to increment the score
            }
        }
    }
}


