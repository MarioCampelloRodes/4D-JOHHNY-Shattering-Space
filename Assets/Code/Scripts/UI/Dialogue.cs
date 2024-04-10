using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel, continueButton;
    public TextMeshProUGUI dialogueText;
    [TextArea(2, 5)] public string[] dialogue;
    private int index;
    public float textSpeed;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if(dialoguePanel.activeSelf) 
            {
                ClearText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                Typing();
            }
        }

        if(dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void ClearText()
    {
        dialogueText.text = string.Empty;
        index = 0;
        dialoguePanel.SetActive(false);
    }

    private void Typing()
    {
        StartCoroutine(TypingCO());
    }

    IEnumerator TypingCO()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }

    public void NextLine()
    {
        continueButton.SetActive(false);

        if(index < dialogue.Length -1)
        {
            index++;
            dialogueText.text = string.Empty;
            Typing();
        }
        else
        {
            ClearText();
        }
    }
}
