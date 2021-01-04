using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text Score;
    public Text Highscore;

    private Save save;

    void Start()
    {
        save = FindObjectOfType<Save>();

        int[] scores = save.GetScores();

        Score.text = "Score: " + scores[0];
        Highscore.text = "Highscore: " + scores[1];
    }

    public void PlayAgainButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void MenuButtonPressed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
