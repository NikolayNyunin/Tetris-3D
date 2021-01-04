using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;

    public void ContinueButtonPressed()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
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
