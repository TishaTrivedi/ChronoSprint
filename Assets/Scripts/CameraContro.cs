using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContro : MonoBehaviour
{
    // Start is called before the first frame update
  // Start is called before the first frame update
//     void Start()
//     {
//         GetComponent<Rigidbody> ().velocity = new Vector3(0,0,4);
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

 public Transform player; // Reference to the player object
    public float smoothSpeed = 0.125f; // Speed at which the camera moves
    public float distanceBehindPlayer = 5f; // Distance behind the player (adjust as needed)

    void FixedUpdate()
    {
        // Calculate the desired position for the camera behind the player
        Vector3 desiredPosition = player.position - player.forward * distanceBehindPlayer;

        // Keep the camera's X and Y positions fixed
        desiredPosition.y = transform.position.y;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Make the camera look at the player
        transform.LookAt(player);
    }
}