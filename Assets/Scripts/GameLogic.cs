using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI components
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    private int score = 0; // Player's score
    private float timer = 0f; // Game timer
    private bool gameEnded = false; // Track if the game has ended

    [SerializeField]
    private Text scoreText; // Reference to the UI Text for the score

    [SerializeField]
    private Text timerText; // Reference to the UI Text for the timer

    [SerializeField]
    private Text gameOverText; // Reference to the UI Text for the game over message

    [SerializeField]
    private float winTime = 300f; // 5 minutes in seconds

    void Start()
    {
        // Initialize the game over text as hidden
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // If the game has ended, stop updating
        if (gameEnded)
        {
            return;
        }

        // Update the timer every frame
        timer += Time.deltaTime;

        // Update the UI text for the timer
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();
        }

        // Check for win condition (5 minutes)
        if (timer >= winTime)
        {
            EndGame("YOU WIN!");
        }
    }

    // Method to increment the score
    public void IncrementScore()
    {
        score++;

        // Update the UI text for the score
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Method to handle game over
    public void EndGame(string message)
    {
        // Freeze the game
        gameEnded = true;
        Time.timeScale = 0f; // Stop all game activity

        // Display the game over message
        if (gameOverText != null)
        {
            gameOverText.text = message + "\nYour score: " + score;
            gameOverText.gameObject.SetActive(true);
        }
    }

    // Detect collisions with the Balloon
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EndGame("GAME OVER");
        }
    }
}
