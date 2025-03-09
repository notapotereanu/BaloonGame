using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonCollision : MonoBehaviour
{

    [SerializeField]
    private GameLogic gameLogic; // Reference to the GameLogic script

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the colliding object is an Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameLogic.EndGame("GAME OVER");
        }
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
