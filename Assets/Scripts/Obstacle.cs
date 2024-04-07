using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Access the ScoreManager script attached to the ScoreManager GameObject
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

            // Check if the ScoreManager script is not null
            if (scoreManager != null)
            {
                // Stop the game
                Time.timeScale = 0;

                // Set score increasing to false
                scoreManager.scoreIncreasing = false;

                // Find GameManager and trigger GameOver if it exists
                GameManager gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.GameOver();
                }
            }
        }
    }
}