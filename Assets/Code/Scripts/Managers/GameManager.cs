using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int highScoreLevelTwo, highScoreLevelThree;

    [Header("Upgrades")]
    public bool hasSpeedUpgrade;
    public bool hasAttackUpgrade, hasJumpUpgrade;

    [Header("Levels Cleared")]
    public bool levelOneClear;
    public bool levelTwoClear, levelThreeClear;

    UIController uiRef;

    IkalController iCRef;

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
        if(SceneManager.GetActiveScene().name == "Level-2" || SceneManager.GetActiveScene().name == "Level-3" || SceneManager.GetActiveScene().name == "Boss")
        {
            uiRef = GameObject.Find("Canvas").GetComponent<UIController>();

            iCRef = GameObject.FindWithTag("Player").GetComponent<IkalController>();
        }     

        LoadData();
    }

    public void SaveLevelScore()
    {
        if(SceneManager.GetActiveScene().name == "Level-2")
            PlayerPrefs.SetInt("ScoreLevel2", uiRef.highScore);
        else if (SceneManager.GetActiveScene().name == "Level-3")
            PlayerPrefs.SetInt("ScoreLevel3", uiRef.highScore);
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

    public void SaveLevelClear()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level-1":
                levelOneClear = true;
                PlayerPrefs.SetInt("LevelOneClear", 1);
                break;
            case "Level-2":
                levelTwoClear = true;
                PlayerPrefs.SetInt("LevelTwoClear", 1);
                break;
            case "Level-3":
                levelThreeClear = true;
                PlayerPrefs.SetInt("LevelThreeClear", 1);
                break;
            default:
                Debug.Log("Este nivel no requiere save o no se ha detectado correctamente");
                break;
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
            highScoreLevelThree = PlayerPrefs.GetInt("ScoreLevel3");
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

        if (SceneManager.GetActiveScene().name == "Level-2" || SceneManager.GetActiveScene().name == "Level-3" || SceneManager.GetActiveScene().name == "Boss")
        {
            if (GameManager.gMRef.hasSpeedUpgrade)
            {
                iCRef.playerSpeed++;
                iCRef.dashSpeed++;
            }

            if (GameManager.gMRef.hasAttackUpgrade)
            {
                iCRef.lightDamage++;
                iCRef.heavyDamage++;
            }

            if (GameManager.gMRef.hasJumpUpgrade)
                iCRef.playerJumpForce++;
        }    

        //Level Clears
        if (PlayerPrefs.HasKey("LevelOneClear") && PlayerPrefs.GetInt("LevelOneClear") == 1)
        {
            levelOneClear = true;
        }

        if (PlayerPrefs.HasKey("LevelTwoClear") && PlayerPrefs.GetInt("LevelTwoClear") == 1)
        {
            levelTwoClear = true;
        }

        if (PlayerPrefs.HasKey("LevelThreeClear") && PlayerPrefs.GetInt("LevelThreeClear") == 1)
        {
            levelThreeClear = true;
        }
    }

    public void ClearData()
    {
        //Highscores

        if (PlayerPrefs.HasKey("ScoreLevel2"))
        {
            highScoreLevelTwo = 0;
            PlayerPrefs.SetInt("ScoreLevel2", highScoreLevelTwo);
        }

        if (PlayerPrefs.HasKey("ScoreLevel3"))
        {
            highScoreLevelThree = 0;
            PlayerPrefs.SetInt("ScoreLevel3", highScoreLevelThree);
        }

        //Upgrades

        if (PlayerPrefs.HasKey("SpeedUpgrade"))
        {
            hasSpeedUpgrade = false;
            PlayerPrefs.SetInt("SpeedUpgrade", 0);
        }

        if (PlayerPrefs.HasKey("AttackUpgrade"))
        {
            hasAttackUpgrade = false;
            PlayerPrefs.SetInt("AttackUpgrade", 0);
        }

        if (PlayerPrefs.HasKey("JumpUpgrade"))
        {
            hasJumpUpgrade = false;
            PlayerPrefs.SetInt("JumpUpgrade", 0);
        }

        //Level Clears

        if (PlayerPrefs.HasKey("LevelOneClear"))
        {
            levelOneClear = false;
            PlayerPrefs.SetInt("LevelOneClear", 0);
        }

        if (PlayerPrefs.HasKey("LevelTwoClear"))
        {
            levelOneClear = false;
            PlayerPrefs.SetInt("LevelTwoClear", 0);
        }

        if (PlayerPrefs.HasKey("LevelThreeClear"))
        {
            levelOneClear = false;
            PlayerPrefs.SetInt("LevelThreeClear", 0);
        }
    }
}
