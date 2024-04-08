using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For scene loading

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public GameObject gameOverUIprefab;


    public float finalScore; // Final score (can remain float for displaying decimals)
    public float highScore;  // High score (now an int)

    void Start()
    {
        isGameOver = false; // Ensure game starts with game not over
        
    }



    public void GameOver()
    {
        isGameOver = true;
        // Show the game over UI
        if (gameOverUIprefab != null)
        {
            GameObject gameOverUI = Instantiate(gameOverUIprefab, transform.position, transform.rotation);

            // Access ScoreManager script in the scene (assuming it's a singleton or accessible)
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                // Access final score and high score from ScoreManager
                finalScore = scoreManager.scoreCount;
                highScore = scoreManager.hiScoreCount;

                // Update Text elements in the UI
                //gameOverUI.GetComponentInChildren<Text>().text = "Game Over!";
                gameOverUI.GetComponentInChildren<Text>().text = "<b>Game Over</b>\nScore: " + Mathf.Round(finalScore);
                RectTransform textRectTransform = gameOverUI.GetComponentInChildren<Text>().GetComponent<RectTransform>();

                // Adjust height to accommodate two lines (optional)
                textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, 25);

                // Adjust vertical positioning of the text within its parent
                textRectTransform.offsetMin = new Vector2(textRectTransform.offsetMin.x, 25); // Replace with estimated line height
            }
            else
            {
                Debug.LogError("ScoreManager not found!"); // Handle potential missing ScoreManager
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Restarting Game!");// Reload current scene
    }
}
