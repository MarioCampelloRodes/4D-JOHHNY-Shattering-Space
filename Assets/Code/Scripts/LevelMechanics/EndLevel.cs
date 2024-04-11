using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private CameraController _cCRef;
    private LSUIControl _uIRef;
    private void Start()
    {
        _cCRef = GameObject.Find("Main Camera").GetComponent<CameraController>();
        _uIRef = GameObject.Find("Canvas").GetComponent<LSUIControl>();
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

        yield return new WaitForSeconds(1f);

        _uIRef.FadeToBlack();

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
}
