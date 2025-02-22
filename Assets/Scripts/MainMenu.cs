using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
        Debug.Log("MainScene loading");
    }


    // Called by the Exit Game button
    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
