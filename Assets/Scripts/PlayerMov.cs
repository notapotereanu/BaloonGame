using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        // Handle movement
        MovePlayer();

        // Handle rotation to look at mouse
        RotateToMouse();
    }

    void MovePlayer()
    {
        // Get input for movement
        float movementX = Input.GetAxis("Vertical"); // W/S
        float movementZ = Input.GetAxis("Horizontal"); // A/D

        // Move the player in world space directions
        Vector3 movement = new Vector3(movementZ, 0, movementX);
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }

    void RotateToMouse()
    {
        // Get the mouse position in screen 
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world space
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); 

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);

            // Rotate the player to look at the point
            Vector3 direction = (point - transform.position).normalized;
            direction.y = 0; 
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}

