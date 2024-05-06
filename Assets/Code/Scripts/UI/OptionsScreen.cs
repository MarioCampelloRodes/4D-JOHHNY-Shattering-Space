using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScreen : MonoBehaviour
{
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
}
