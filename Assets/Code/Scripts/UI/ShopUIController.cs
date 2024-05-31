using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopUIController : MonoBehaviour
{
    public int money;

    public int speedPrice, attackPrice, jumpPrice;

    public Button speedButton, attackButton, jumpButton;

    public TextMeshProUGUI coinAmountText;

    private void Start()
    {
        GameManager.gMRef.LoadData();
        
        money = GameManager.gMRef.highScoreLevelTwo + GameManager.gMRef.highScoreLevelThree;

        coinAmountText.text = money.ToString();

        LoadUpgrades();
    }
    public void PurchaseSpeedUpgrade()
    {
        if(money >= speedPrice)
        {
            Debug.Log("Velocidad aumentada");
            GameManager.gMRef.hasSpeedUpgrade = true;
            speedButton.image.color = new Color(0.5f, 1f, 0.5f, 0.5f);
        }
    }

    public void PurchaseAttackUpgrade()
    {
        if (money >= attackPrice)
        {
            Debug.Log("Ataque aumentado");
            GameManager.gMRef.hasAttackUpgrade = true;
            attackButton.image.color = new Color(0.5f, 1f, 0.5f, 0.5f);
        }
    }

    public void PurchaseJumpUpgrade()
    {
        if(money >= jumpPrice)
        {
            Debug.Log("Salto aumentado");
            GameManager.gMRef.hasJumpUpgrade = true;
            jumpButton.image.color = new Color(0.5f, 1f, 0.5f, 0.5f);
        }
    }

    public void LoadUpgrades()
    {
        if (GameManager.gMRef.hasSpeedUpgrade)
        {
            speedButton.image.color = new Color(0.5f, 1f, 0.5f, 0.5f);
        }

        if (GameManager.gMRef.hasAttackUpgrade)
        {
            attackButton.image.color = new Color(0.5f, 1f, 0.5f, 0.5f);
        }

        if (GameManager.gMRef.hasJumpUpgrade)
        {
            jumpButton.image.color = new Color(0.5f, 1f, 0.5f, 0.5f);
        }
    }

    public void GoToLevelSelector()
    {
        GameManager.gMRef.SaveUpgrades();

        SceneManager.LoadScene("LevelSelector");
    }
}
