using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject life1, life2, life3, life4, life5, life6;

    public int score, time, streak, highStreak, highScore;

    public int timeBonus, streakBonus;

    private float _timeCounter;

    public TextMeshProUGUI scoreText, timeText, streakText;

    public GameObject pauseScreen;

    private PlayerHealthController _pHRef;
    // Start is called before the first frame update
    void Start()
    {
        _pHRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();

        _timeCounter = 0;
    }

    private void Update()
    {
        _timeCounter += Time.deltaTime;

        if (_timeCounter >= 1)
        {
            time++;
            _timeCounter = 0;

            timeText.text = time.ToString();
        }

        if(Input.GetButtonDown("Escape"))
        {
            ActivatePauseScreen();
            Time.timeScale = 0;
        }
    }

    public void AddScore(int points)
    {
        score += points;

        scoreText.text = score.ToString();
    }

    public void AddStreak()
    {
        streak++;

        if(streak > highStreak)
        {
            highStreak = streak;
        }

        streakText.text = streak.ToString();
    }
    public void ResetStreak()
    {
        streak = 0;
        streakText.text = streak.ToString();
    }

    public void UpdateHealth()
    {
        switch (_pHRef.currentHealth)
        {
            case 6:
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(true);
                life6.SetActive(true);
                break;
            }

            case 5:
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(true);
                life6.SetActive(false);
                break;
            }

            case 4:
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(true);
                life5.SetActive(false);
                life6.SetActive(false);
                break;
            }

            case 3:
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(true);
                life4.SetActive(false);
                life5.SetActive(false);
                life6.SetActive(false);
                break;
            }
            case 2:
            {
                life1.SetActive(true);
                life2.SetActive(true);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
                life6.SetActive(false);
                break;
            }
            case 1:
            {
                life1.SetActive(true);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
                life6.SetActive(false);
                break;
            }
            case 0:
            {
                life1.SetActive(false);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
                life6.SetActive(false);
                break;
            }
            default:
            {
                life1.SetActive(false);
                life2.SetActive(false);
                life3.SetActive(false);
                life4.SetActive(false);
                life5.SetActive(false);
                life6.SetActive(false);
                break;
            }
        }
    }

    public void CalculateFinalScore()
    {
        

        if(time < 120)
        {
            timeBonus = 5000;
        }
        else if(time < 180)
        {
            timeBonus = 2000;
        }
        else
        {
            timeBonus = 0;
        }

        if(highStreak > 10)
        {
            streakBonus = 5000;
        }
        else if(highStreak > 5)
        {
            streakBonus = 2000;
        }
        else
        {
            streakBonus = 0;
        }

        Debug.Log("Has completado el nivel en " + time + " segundos, obtienes un bonus de " + timeBonus + " puntos");
        Debug.Log("Has conseguido una racha de " + highStreak + " enemigos, obtienes un bonus de " + streakBonus + " puntos");
        highScore = score + timeBonus + streakBonus;
        Debug.Log("La puntuación total es de " + highScore);

        if(SceneManager.GetActiveScene().name == "Level-2" && highScore > GameManager.gMRef.highScoreLevelTwo)
        {
            Debug.Log("La puntuación es mayor, se guardará");
            GameManager.gMRef.SaveLevelScore();
        }
        else if(SceneManager.GetActiveScene().name == "Level-3" && highScore > GameManager.gMRef.highScoreLevelThree)
        {
            Debug.Log("La puntuación es mayor, se guardará");
            GameManager.gMRef.SaveLevelScore();
        }
    }

    public void ActivatePauseScreen()
    {
        pauseScreen.SetActive(true);
    }
}
