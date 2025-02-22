using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Pause Menu Panel")]
    [SerializeField] private GameObject pauseMenuPanel;

    private bool isPaused = false;

    void Start()
    {
        // Game starts immediately—ensure time is running and the pause menu is hidden.
        Time.timeScale = 1f;
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        // Listen for the pause toggle (P key)
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    // Toggles pause/resume
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            // Pause game: stop time and show the pause menu.
            Time.timeScale = 0f;
            if (pauseMenuPanel != null)
                pauseMenuPanel.SetActive(true);
        }
        else
        {
            // Resume game: restart time and hide the pause menu.
            Time.timeScale = 1f;
            if (pauseMenuPanel != null)
                pauseMenuPanel.SetActive(false);
        }
    }

    // Called by the Resume button
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
    }

    // Called by the Load Game button
    public void LoadGame()
    {
        // Place your load game logic here.
        // For demonstration, we log a message and resume the game.
        Debug.Log("Load Game pressed.");
        ResumeGame();
    }

    // Called by the Exit Game button
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting Game");
    }
}
