using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    //[SerializeField]
    float movementSpeed = 5f;


    void Update()
    {
        // Handle movement
        MovePlayer();

        // Handle flipping based on mouse position
        FlipSprite();
    }

    void MovePlayer()
    {
        // Get input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Move the player in 2D space
        Vector2 movement = new Vector2(moveX, moveY);
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }

    public void FlipSprite()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool flipped;
        // Check if the mouse is to the left or right of the player
        if (mousePosition.x < transform.position.x)
        {
            // Flip the sprite to face left
            transform.localScale = new Vector3(-1, 1, 1);
            flipped = true;
        }
        else
        {
            // Flip the sprite to face right
            transform.localScale = new Vector3(1, 1, 1);
            flipped = false;
        }
    }
}


