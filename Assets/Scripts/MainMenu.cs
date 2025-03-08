using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenuCanvas;  // Assign in Inspector
    public GameObject loadLevelCanvas; // Assign in Inspector
    public GameObject guideLevelCanvas; // Assign in Inspector

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level1");
        Debug.Log("MainScene loading");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadSceneAsync("Level1");
        Debug.Log("Level 1 loading");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadSceneAsync("Level2");
        Debug.Log("Level 2 loading");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadSceneAsync("Level3");
        Debug.Log("Level 3 loading");
    }

    public void ToggleGuideButton()
    {
        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);

        if (loadLevelCanvas != null)
            loadLevelCanvas.SetActive(!loadLevelCanvas.activeSelf);

        if (guideLevelCanvas != null)
            guideLevelCanvas.SetActive(!guideLevelCanvas.activeSelf);
    }

    // Called by the Exit Game button
    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
