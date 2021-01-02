using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
