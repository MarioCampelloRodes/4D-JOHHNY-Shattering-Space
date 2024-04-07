using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUIController : MonoBehaviour
{
    public void GoToLevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}
