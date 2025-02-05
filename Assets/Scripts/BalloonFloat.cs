using System;
using UnityEngine;
using static UnityEngine.Scripting.GarbageCollector;

public class BalloonFloat : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float force;

    [SerializeField]
    private Transform hand;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Sway();
        
    }

    void Move()
    {
        rb.AddForce(new Vector2((hand.position.x - rb.position.x), (hand.position.y - rb.position.y)) * force);

        float maxY = hand.position.y + 8f;
        float minY = hand.position.y + 6f; 

        // Constrain on Y
        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, minY);
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

    }
    void Sway()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
    
        if (moveX == 0 && moveY == 0 && Math.Floor(Time.time) % 17 == 0)
        {
            rb.AddForce(new Vector2(1, 0) * force * 0.5f);
        }
    }
}






