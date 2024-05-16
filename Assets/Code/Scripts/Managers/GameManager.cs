using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIController uiRef;
    // Start is called before the first frame update
    void Start()
    {
        uiRef = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Score", uiRef.score);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.GetInt("Score", uiRef.score);
        }
    }
}
