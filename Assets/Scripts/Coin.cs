using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnSpeed = 90f;
    public int scoreToAdd = 50; // Adjust the score to add when collecting the coin

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
                // Add the scoreToAdd value to the score
                scoreManager.addScore(scoreToAdd);
            }

            // Destroy this coin object
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
