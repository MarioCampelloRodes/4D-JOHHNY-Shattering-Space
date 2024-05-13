using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject optionsScreen;
    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            DeactivatePauseScreen();
        }
    }
    // Start is called before the first frame update
    public void DeactivatePauseScreen()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void ActivateOptionsScreen()
    {
        optionsScreen.SetActive(true);
    }

    public void GoToLevelSelector()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelSelector");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}