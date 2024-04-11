using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialObject;

    [TextArea(14,10)] public string tutorialText;

    private bool isActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            tutorialObject.SetActive(true);
            tutorialObject.GetComponentInChildren<TextMeshProUGUI>().text = tutorialText;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DeactivateTutorialCO());
        }
    }

    IEnumerator DeactivateTutorialCO()
    {
        yield return new WaitForSeconds(3f);

        isActive = true;

        tutorialObject.SetActive(false);
    }
}
