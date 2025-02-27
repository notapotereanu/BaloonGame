using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed; // Movement speed of the player

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Handle flipping based on mouse position
        FlipSprite();
    }

    void FixedUpdate()
    {
        // Get input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveY = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Calculate the movement vector
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Apply movement using velocity
        rb.velocity = movement * movementSpeed;
    }

    void FlipSprite()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the mouse is to the left or right of the player
        if (mousePosition.x < transform.position.x)
        {
            // Flip the sprite to face left
            transform.localScale = new Vector3(-0.33f, 0.33f, 1);
        }
        else
        {
            // Flip the sprite to face right
            transform.localScale = new Vector3(0.33f, 0.33f, 1);
        }
    }
}


