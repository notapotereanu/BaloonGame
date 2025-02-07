using System;
using UnityEngine;

public class BalloonFloat : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float force;

    [SerializeField]
    private Transform hand;

    float offset;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        offset = rb.position.y - hand.position.y;
    }

    void FixedUpdate()
    {
        Move();
        Sway();
    }

    void Move()
    {
        // Base Movement
        rb.AddForce(new Vector2((hand.position.x - rb.position.x), (hand.position.y - rb.position.y)) * force);

        float maxY = hand.position.y + (offset * 1.1f);
        float minY = hand.position.y + offset;

        float maxX = hand.position.x + offset;
        float minX = hand.position.x + offset * -1.0f;


        //  Restricts balloon from falling too far behind on Y movement
        if (transform.position.y > maxY) // Down
        {
            transform.position = new Vector2(transform.position.x, maxY);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else if (transform.position.y < minY) // Up
        {
            transform.position = new Vector2(transform.position.x, minY);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }


        /*
        * Note
        * I kind of think Force looks better on X & Transform on Y. 
        * I left a transform option in for everyone else to try
        */
        //  Restricts balloon from falling too far behind on X movement
        if (transform.position.x > maxX) // Left
        {
            // Option 1
            rb.AddForce(new Vector2(-1, 0) * force * 5f);

            // option 2
            //transform.position = new Vector2(maxX, transform.position.y);
            //rb.velocity = new Vector2(0f, rb.velocity.x);
        }
        else if (transform.position.x < minX) // Right
        {
            rb.AddForce(new Vector2(1, 0) * force * 5f);
        }
    }


    // Adds a swaying motion when child is not moving
    void Sway()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (moveX == 0 && moveY == 0 && Math.Floor(Time.time) % 13 == 0)
        {
            rb.AddForce(new Vector2(1, 0) * force * 0.5f);
        }
    }
}