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
    private bool isPaused = false; // Track pause state

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

    [SerializeField]
    private GameObject pauseOverlay; // UI element for the pause overlay

    [SerializeField]
    private AudioSource backgroundMusic; // Background music AudioSource

    [SerializeField]
    private AudioSource gameOverSong; // Game Over song AudioSource

    // Static variable to store lives across levels
    public static int lives = 2;

    void Start()
    {
        // Initialize the game over text and buttons as hidden
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
        // Hide the pause overlay at the start
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(false);
        }
        // Update the lives UI at the start of the scene
        UpdateLivesUI();

        // Ensure the Game Over song is not playing at the start
        if (gameOverSong != null)
        {
            gameOverSong.Stop();
        }
    }

    void Update()
    {
        // Check for pause/unpause input (P or Space)
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }

        // If the game is paused or ended, do not update further game logic
        if (isPaused || gameEnded)
        {
            return;
        }

        // Update the timer every frame
        timer += Time.deltaTime;

        // Update the UI text for lives
        if (livesText != null)
        {
            livesText.text = "Lives: " + (lives + 1);
        }

        // Update the UI text for the timer
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();
        }

        // Check for win condition 
        if (timer >= winTime)
        {
            EndGame("YOU WIN!", true); // Pass true to indicate a win
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
        if (gameEnded)
        {
            return;
        }

        // Freeze the game
        gameEnded = true;
        Time.timeScale = 0f; // Stop all game activity

        // Stop the background music
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        // Play the Game Over song in a loop if the player loses
        if (!isWin && gameOverSong != null)
        {
            gameOverSong.loop = true;
            gameOverSong.Play();
        }

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
        // Show the try again button if it's not a win
        if (!isWin && tryAgainButton != null)
        {
            tryAgainButton.gameObject.SetActive(true);
        }
    }

    // Method to load the next level
    public void GoToNextLevel()
    {
        // Stop the Game Over song if it's playing
        if (gameOverSong != null)
        {
            gameOverSong.Stop();
        }

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
        // Stop the Game Over song if it's playing
        if (gameOverSong != null)
        {
            gameOverSong.Stop();
        }

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
            // Load level 1 and reset lives
            SceneManager.LoadScene("MainMenu");
            lives = 3;
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

    // Toggle between pause and resume
    private void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Pauses the game, sets timescale to 0 and shows the pause overlay
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(true);
        }

        // Pause the background music
        if (backgroundMusic != null)
        {
            backgroundMusic.Pause();
        }
    }

    // Resumes the game, resets timescale and hides the pause overlay
    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(false);
        }

        // Resume the background music
        if (backgroundMusic != null)
        {
            backgroundMusic.UnPause();
        }
    }
}
