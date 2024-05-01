using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialObject;

    public Sprite tutorialCurrentSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorialObject.SetActive(true);
            tutorialObject.GetComponentInChildren<Image>().sprite = tutorialCurrentSprite;
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
