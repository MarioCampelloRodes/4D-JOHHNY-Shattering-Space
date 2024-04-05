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

            LoadLS();
        }
    }

    public void LoadLS()
    {
        StartCoroutine(LoadLSCO());
    }
    private IEnumerator LoadLSCO()
    {
        _cCRef.isFreezed = true;

        yield return new WaitForSeconds(1f);

        _uIRef.FadeToBlack();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("LevelSelector");
    }
}
