using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartGame()
    {
        SceneManager.LoadScene("TryScene");
        Debug.Log("Restarting Game!");// Reload current scene
    }
}
