using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int highScoreLevelTwo, highScoreLevelThree;
    UIController uiRef;

    public static GameManager gMRef;
    private void Awake()
    {
        //Si la referencia del Singleton esta vacía
        if (gMRef == null)
            //La rrellenamos con todo el contenido de este código (para que todo sea accesible)
            gMRef = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiRef = GameObject.Find("Canvas").GetComponent<UIController>();

        highScoreLevelTwo = PlayerPrefs.GetInt("LevelScore2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveLevelScore()
    {
        if(SceneManager.GetActiveScene().name == "Level-2")
            PlayerPrefs.SetInt("ScoreLevel2", uiRef.score);
        if (SceneManager.GetActiveScene().name == "Level-3")
            PlayerPrefs.SetInt("ScoreLevel3", uiRef.score);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.GetInt("Score", uiRef.score);
        }
    }
}
