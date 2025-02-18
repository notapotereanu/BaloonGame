using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class ButtonClick : MonoBehaviour
{
    [SerializeField]
    private GameLogic gameLogic; // Reference to the GameLogic script

    // Method to load the next level
    public void GoToNextLevel()
    {
        gameLogic.GoToNextLevel(); // Call the GoToNextLevel method in GameLogic
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
