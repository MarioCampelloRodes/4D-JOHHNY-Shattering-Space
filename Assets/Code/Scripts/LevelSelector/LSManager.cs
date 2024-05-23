using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    private LSPlayer _lSPRef;

    private LSUIControl _lSUICRef;
    // Start is called before the first frame update
    void Start()
    {
        _lSPRef = GameObject.Find("LSPlayer").GetComponent<LSPlayer>();
        _lSUICRef = GameObject.Find("LSCanvas").GetComponent<LSUIControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        if (_lSPRef.currentPoint.levelToLoad == "Level-2" && GameManager.gMRef.levelOneClear)
            StartCoroutine(LoadLevelCO());
        else if(_lSPRef.currentPoint.levelToLoad == "Level-3" && GameManager.gMRef.levelTwoClear)
            StartCoroutine(LoadLevelCO());
        else if (_lSPRef.currentPoint.levelToLoad == "Boss" && GameManager.gMRef.levelThreeClear)
            StartCoroutine(LoadLevelCO());
        else if (_lSPRef.currentPoint.levelToLoad == "Shop" && GameManager.gMRef.levelTwoClear)
            StartCoroutine(LoadLevelCO());
        else if(_lSPRef.currentPoint.levelToLoad == "Level-1")
            StartCoroutine(LoadLevelCO());
    }

    private IEnumerator LoadLevelCO()
    {
        _lSPRef.canMove = false;

        _lSUICRef.FadeToBlack();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_lSPRef.currentPoint.levelToLoad);
    }
}
