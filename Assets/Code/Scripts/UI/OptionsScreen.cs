using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    public GameObject keyboardMapping, gamepadMapping;
    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            DeactivateOptionsScreen();
        }
    }
    // Start is called before the first frame update
    public void DeactivateOptionsScreen()
    {
        gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ShowKeyboardMapping()
    {
        if (!keyboardMapping.activeInHierarchy)
        {
            keyboardMapping.SetActive(true);
        }

        if (gamepadMapping.activeInHierarchy)
        {
            gamepadMapping.SetActive(false);
        }
    }

    public void ShowGamepadMapping()
    {
        if (!gamepadMapping.activeInHierarchy)
        {
            gamepadMapping.SetActive(true);
        }

        if (keyboardMapping.activeInHierarchy)
        {
            keyboardMapping.SetActive(false);
        }
    }
}
