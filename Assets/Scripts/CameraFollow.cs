using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player; // Reference to the player's transform

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, -10); // Camera offset from the player

    [SerializeField]
    private float smoothSpeed = 0.125f; // Smoothing speed for camera movement

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = player.position + offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}
