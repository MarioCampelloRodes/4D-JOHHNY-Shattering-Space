using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject life1, life2, life3, life4, life5, life6;

    public int score, time, streak;

    private float _timeCounter;

    public TextMeshProUGUI scoreText, timeText, streakText;

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
    }

    public void AddScore(int points)
    {
        score += points;

        scoreText.text = score.ToString();
    }

    public void AddStreak()
    {
        streak++;
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
}
