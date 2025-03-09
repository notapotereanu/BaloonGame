using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 3; // Maximum health of the enemy

    private int currentHealth; // Current health of the enemy

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    // Method to set the health of the enemy
    public void SetHealth(int health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by damage amount

        // Check if health has reached zero
        if (currentHealth <= 0)
        {
            Die(); // Destroy the enemy
        }
    }

    // Method to handle enemy death
    private void Die()
    {
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    // Method to check if the enemy is dead
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}