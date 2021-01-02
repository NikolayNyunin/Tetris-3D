using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle FullScreen;
    public Dropdown Resolutions;

    public GameObject MainMenu, SettingsMenu;

    private bool fullScreen;
    private List<string> resolutions;

    void Start()
    {
        fullScreen = Screen.fullScreen;
        FullScreen.isOn = fullScreen;

        resolutions = new List<string>();

        foreach (Resolution res in Screen.resolutions)
            resolutions.Add(res.width + "x" + res.height + " " + res.refreshRate + "Hz");

        Resolutions.ClearOptions();
        Resolutions.AddOptions(resolutions);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            BackButtonPressed();
    }

    public void FullScreenToggled(bool full)
    {
        fullScreen = full;
        Screen.fullScreen = fullScreen;
    }

    public void ChangeResolution(int resIndex)
    {
        Resolution res = Screen.resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, fullScreen, res.refreshRate);
    }

    public void BackButtonPressed()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
}
