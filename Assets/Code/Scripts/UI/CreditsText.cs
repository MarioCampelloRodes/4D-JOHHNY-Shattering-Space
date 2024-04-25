using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsText : MonoBehaviour
{
    public float creditsSpeed;

    private bool canExit;

    private LSUIController _lSUIRef;

    public GameObject exitButton;

    private void Start()
    {
        _lSUIRef = GameObject.Find("Canvas").GetComponent<LSUIController>();

        CanExitCredits();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + creditsSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space))
        {
            ExitCredits();
        }
    }

    public void ExitCredits()
    {
        if(canExit)
        {
            StartCoroutine(ExitCreditsCO());
        }
    }
    IEnumerator ExitCreditsCO()
    {
        _lSUIRef.FadeToBlack();

        exitButton.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("MainMenu");
    }
    private void CanExitCredits()
    {
        StartCoroutine(CanExitCreditsCO());
    }

    IEnumerator CanExitCreditsCO()
    {
        yield return new WaitForSeconds(4f);

        canExit = true;

        exitButton.SetActive(true);

        yield return new WaitForSeconds(11.5f);

        _lSUIRef.FadeToBlack();

        exitButton.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("MainMenu");
    }
}