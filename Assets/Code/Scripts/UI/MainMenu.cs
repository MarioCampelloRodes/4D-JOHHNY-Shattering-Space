using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsScreen;
    public void NewGame()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void Continue()
    {
        // (Por implementar)
        SceneManager.LoadScene("LevelSelector");
    }

    public void Options()
    {
        ActivateOptionsScreen();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ActivateOptionsScreen()
    {
        optionsScreen.SetActive(true);
    }
}
