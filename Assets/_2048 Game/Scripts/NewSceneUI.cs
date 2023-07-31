using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewSceneUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Retrieve the last scene score from PlayerPrefs and display it.
        int lastSceneScore = PlayerPrefs.GetInt("LastSceneScore", 0);
        scoreText.text = lastSceneScore.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
