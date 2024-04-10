using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialObject;

    [TextArea(14,10)] public string tutorialText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialObject.SetActive(true);
            tutorialObject.GetComponentInChildren<TextMeshProUGUI>().text = tutorialText;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialObject.SetActive(false);
        }
    }
}
