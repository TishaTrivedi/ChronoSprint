using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public float zOffset = -15f; // Offset behind the player along the z-axis
    public float smoothSpeed = 10f; // Smoothing factor for the camera movement

    void LateUpdate()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player transform not assigned.");
            return;
        }

        // Calculate the desired position of the camera
        Vector3 playerPosition = playerTransform.position;
        Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, playerPosition.z + zOffset);

        // Move the player horizontally with the camera's x-position remaining constant
        playerPosition.x = transform.position.x;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}