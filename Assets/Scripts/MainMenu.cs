using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
