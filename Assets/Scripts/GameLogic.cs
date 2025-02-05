using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class GameLogic : MonoBehaviour
{
    private int score = 0; // Player's score
    private float timer = 0f; // Game timer

    [SerializeField]
    private Text scoreText; // Reference to the UI Text for the score

    [SerializeField]
    private Text timerText; // Reference to the UI Text for the timer

    void Update()
    {
        // Update the timer every frame
        timer += Time.deltaTime;

        // Update the UI text for the timer
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.FloorToInt(timer).ToString();
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
}
