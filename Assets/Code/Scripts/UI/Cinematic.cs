using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    private LSUIController _lSUIRef;

    public GameObject exitButton;

    private void Start()
    {
        _lSUIRef = GameObject.Find("Canvas").GetComponent<LSUIController>();

        StartCoroutine(EndCinematic());
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
        {
            exitButton.SetActive(true);
        }
    }

    public void SkipCinematic()
    {
        StartCoroutine(SkipCinematicCO());
    }

    IEnumerator SkipCinematicCO()
    {
        _lSUIRef.FadeToBlack();

        exitButton.SetActive(false);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Level-1");
    }

    IEnumerator EndCinematic()
    {
        yield return new WaitForSeconds(42f);

        SceneManager.LoadScene("Level-1");
    }
}
