using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
        Debug.Log("MainScene loading");
        // Game starts paused—ensure time is stopped and the pause menu is shown.
        //Time.timeScale = 0f;
        //isPaused = true; // Set isPaused to true to indicate the game is starting paused.
        //if (pauseMenuPanel != null)
        //    pauseMenuPanel.SetActive(true);
    }


    // Called by the Exit Game button
    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
