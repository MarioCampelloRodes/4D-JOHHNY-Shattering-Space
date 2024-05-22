using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int highScoreLevelTwo, highScoreLevelThree;

    public bool hasSpeedUpgrade, hasAttackUpgrade, hasJumpUpgrade;
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

        LoadData();
    }

    private void Update()
    {
        //Prueba
        if (Input.GetButtonDown("Jump"))
        {
            SaveLevelScore();
        }
    }

    public void SaveLevelScore()
    {
        if(SceneManager.GetActiveScene().name == "Level-2")
            PlayerPrefs.SetInt("ScoreLevel2", uiRef.score);
        else if (SceneManager.GetActiveScene().name == "Level-3")
            PlayerPrefs.SetInt("ScoreLevel3", uiRef.score);
    }

    public void SaveUpgrades()
    {
        if (hasSpeedUpgrade)
        {
            PlayerPrefs.SetInt("SpeedUpgrade", 1);
        }
        if (hasAttackUpgrade)
        {
            PlayerPrefs.SetInt("AttackUpgrade", 1);
        }
        if (hasJumpUpgrade)
        {
            PlayerPrefs.SetInt("JumpUpgrade", 1);
        }
    }

    public void LoadData()
    {
        //Highscores

        if (PlayerPrefs.HasKey("ScoreLevel2"))
        {
            highScoreLevelTwo = PlayerPrefs.GetInt("ScoreLevel2");
        }

        if (PlayerPrefs.HasKey("ScoreLevel3"))
        {
            highScoreLevelThree = PlayerPrefs.GetInt("ScoreLevel2");
        }

        //Upgrades

        if (PlayerPrefs.HasKey("SpeedUpgrade") && PlayerPrefs.GetInt("SpeedUpgrade") == 1)
        {
            hasSpeedUpgrade = true;
        }

        if (PlayerPrefs.HasKey("AttackUpgrade") && PlayerPrefs.GetInt("AttackUpgrade") == 1)
        {
            hasAttackUpgrade = true;
        }

        if (PlayerPrefs.HasKey("JumpUpgrade") && PlayerPrefs.GetInt("JumpUpgrade") == 1)
        {
            hasJumpUpgrade = true;
        }
    }

    public void ClearData()
    {
        //Highscores

        if (PlayerPrefs.HasKey("ScoreLevel2"))
        {
            highScoreLevelTwo = 0;
        }

        if (PlayerPrefs.HasKey("ScoreLevel3"))
        {
            highScoreLevelThree = 0;
        }

        //Upgrades

        if (PlayerPrefs.HasKey("SpeedUpgrade"))
        {
            hasSpeedUpgrade = false;
        }

        if (PlayerPrefs.HasKey("AttackUpgrade"))
        {
            hasAttackUpgrade = false;
        }

        if (PlayerPrefs.HasKey("JumpUpgrade"))
        {
            hasJumpUpgrade = false;
        }
    }
}
