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
                // Stop the game and display the score
                scoreManager.scoreIncreasing = false;

                // Call the GameOver method in MenuController to display the final score and reload the scene
                //    MenuController menuController = FindObjectOfType<MenuController>();
                //    if (menuController != null)
                //    {
                //        menuController.GameOver(Mathf.RoundToInt(scoreManager.scoreCount));
                //    }
                //}
            }
        }
    }
}