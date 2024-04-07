using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneUIController : MonoBehaviour
{
    public GameObject pauseScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivatePauseScreen();
            Time.timeScale = 0;
        }
    }

    public void ActivatePauseScreen()
    {
        pauseScreen.SetActive(true);
    }
}
