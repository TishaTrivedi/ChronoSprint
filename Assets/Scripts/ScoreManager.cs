using UnityEngine;
using UnityEngine.UI;

// Option 1: Using static class reference (consider for short-term use)
using static UnityEngine.PlayerPrefs;  // Uncomment this line if using static reference

// Option 2: Explicit class reference (recommended for clarity)
//using UnityEngine.PlayerPrefs; // Import for PlayerPrefs

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public Text hiScoreText;

    public float scoreCount;
    public float hiScoreCount;

    public float pointsPerSeconds;

    public bool scoreIncreasing;

    // Start is called before the first frame update
    void Start()
    {
        // Load previously saved high score (choose either Option 1 or Option 2)

        // Option 1 (using static reference)
        hiScoreCount = GetFloat("highScore", 0); // 0 is the default value if no high score is saved

        // Option 2 (explicit class reference)
        /*hiScoreCount = PlayerPrefs.GetFloat("highScore", 0);*/ // 0 is the default value if no high score is saved
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSeconds * Time.deltaTime;
        }

        if (scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
            PlayerPrefs.SetFloat("highScore", hiScoreCount); // Save new high score
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        hiScoreText.text = "High Score: " + Mathf.Round(hiScoreCount);
    }

    public void addScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }
}