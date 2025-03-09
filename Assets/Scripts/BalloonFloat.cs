using System;
using UnityEngine;


public class BalloonFloat : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float force;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Bob();
    }

   
    void Bob()
    {
        float moveY = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        if (moveX == 0 && moveY == 0)
        {
            rb.AddForce(Vector2.down * Mathf.Sin(Time.time) * force);
        }
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
        }
    }
}