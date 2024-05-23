using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndLevel : MonoBehaviour
{
    public GameObject endScreen, endScoreTitle, endTotalScoreTitle;
    public TextMeshProUGUI highscoreText, timeBonusText, streakBonusText, totalScoreText, levelTwoText, levelThreeText;

    private CameraController _cCRef;
    private UIController _uIRef;
    private LSUIControl _lsUIRef;
    private void Start()
    {
        _cCRef = GameObject.Find("Main Camera").GetComponent<CameraController>();
        _lsUIRef = GameObject.Find("Canvas").GetComponent<LSUIControl>();
        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "Level-1")
            {
                collision.GetComponent<JohnnyController>().canMove = false;
            }
            else
            {
                collision.GetComponent<IkalController>().isLevelOver = true;
            }

            ExitLevel();
        }
    }

    public void ExitLevel()
    {
        StartCoroutine(ExitLevelCO());
    }
    private IEnumerator ExitLevelCO()
    {
        _cCRef.isFreezed = true;

        if(SceneManager.GetActiveScene().name != "Level-1")
            _uIRef.CalculateFinalScore();

        GameManager.gMRef.SaveLevelClear();

        ShowWinText();

        yield return new WaitForSeconds(2f);

        _lsUIRef.FadeToBlack();

        yield return new WaitForSeconds(1f);
        
        if(SceneManager.GetActiveScene().name == "Boss")
        {
            SceneManager.LoadScene("Credits");
        }
        else
        {
            SceneManager.LoadScene("LevelSelector");
        }
    }

    void ShowWinText()
    {
        endScreen.SetActive(true);

        if(SceneManager.GetActiveScene().name == "Level-2" || SceneManager.GetActiveScene().name == "Level-3")
        {
            endScoreTitle.SetActive(true);
            timeBonusText.text = _uIRef.timeBonus.ToString();
            streakBonusText.text = _uIRef.streakBonus.ToString();
            highscoreText.text = _uIRef.highScore.ToString();
        }
        else if(SceneManager.GetActiveScene().name == "Boss")
        {
            endTotalScoreTitle.SetActive(true);
            totalScoreText.text = (GameManager.gMRef.highScoreLevelTwo + GameManager.gMRef.highScoreLevelThree).ToString();
            levelTwoText.text = GameManager.gMRef.highScoreLevelTwo.ToString();
            levelThreeText.text = GameManager.gMRef.highScoreLevelThree.ToString();
        }
    }
}
