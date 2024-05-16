using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUIController : MonoBehaviour
{
    public void PurchaseSpeedUpgrade()
    {
        Debug.Log("Velocidad aumentada");
    }

    public void PurchaseAttackUpgrade()
    {
        Debug.Log("Ataque aumentado");
    }

    public void PurchaseJumpUpgrade()
    {
        Debug.Log("Salto aumentado");
    }

    public void GoToLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
