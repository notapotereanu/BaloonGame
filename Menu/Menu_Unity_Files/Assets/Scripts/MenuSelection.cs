using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    public void NewGame( )
    {
        Debug.Log("Game starting");
    }

    public void LoadGame( )
    {
        Debug.Log("Game loading");
    }

    public void ExitGame( )
    {
        Debug.Log("Exiting");
    }
}
