using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    private int score = 0; // Player's score
    private float timer = 0f; // Game timer
    private bool gameEnded = false; // Track if the game has ended

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Button nextLevelButton;

    [SerializeField]
    private Button tryAgainButton;

    [SerializeField]
    private float winTime = 300f; // 5 minutes in seconds 

    [SerializeField]
    private PlayerShot playerShot;

    [SerializeField]
    private PlayerMov playerMov;

    // Static variable to store lives across levels
    public static int lives = 3;

    void Start()
    {
        // Initialize the game over text and button as hidden
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        if (nextLevelButton != null)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
        if (tryAgainButton != null)
        {
            tryAgainButton.gameObject.SetActive(false);
        }

        // Update the lives UI at the start of the scene
        UpdateLivesUI();
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
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }

        // Update the UI text for the timer
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();
        }

        // Check for win condition 
        if (!gameEnded && timer >= winTime)
        {
            EndGame("YOU WIN!", true); // Pass true to indicate a win
            gameEnded = true; // Ensure the game ends immediately
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

    public void decreaseLives()
    {
        lives--;
        UpdateLivesUI(); // Update the lives UI when lives decrease
    }

    // Method to handle game over
    public void EndGame(string message, bool isWin = false)
    {
        // If the game has already ended, return
        if (gameEnded)
        {
            return;
        }

        // Freeze the game
        gameEnded = true;
        Time.timeScale = 0f; // Stop all game activity

        // Disable the PlayerShot and PlayerMov scripts
        if (playerShot != null)
        {
            playerShot.enabled = false;
        }
        if (playerMov != null)
        {
            playerMov.enabled = false;
        }

        // Display the game over message
        if (gameOverText != null)
        {
            gameOverText.text = message + "\nYour score: " + score;
            gameOverText.gameObject.SetActive(true);
        }

        // Show the "Go to next level" button if the player wins and it's not Level3
        if (isWin && nextLevelButton != null)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene != "Level3")
            {
                nextLevelButton.gameObject.SetActive(true);
            }
        }
        if (isWin != true && tryAgainButton != null)
        {
            tryAgainButton.gameObject.SetActive(true);
        }
    }

    // Method to load the next level
    public void GoToNextLevel()
    {
        Time.timeScale = 1f; // Unfreeze the game

        // Get the current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Determine the next level based on the current scene
        string nextLevel = "";
        switch (currentScene)
        {
            case "Level1":
                nextLevel = "Level2";
                break;
            case "Level2":
                nextLevel = "Level3";
                break;
            default:
                Debug.LogWarning("No next level defined for: " + currentScene);
                return;
        }

        // Load the next level
        SceneManager.LoadScene(nextLevel);
    }

    // Method to try again if the player has lost
    public void TryAgain()
    {
        Time.timeScale = 1f; // Unfreeze the game

        // Get the current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        if (lives > 0)
        {
            lives--;
            UpdateLivesUI(); 
            // Load the current level
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            // Load level 1
            SceneManager.LoadScene("Level1");
            lives = 3; // Reset lives 
            UpdateLivesUI(); 
        }
    }

    // Method to update the lives UI
    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }
}