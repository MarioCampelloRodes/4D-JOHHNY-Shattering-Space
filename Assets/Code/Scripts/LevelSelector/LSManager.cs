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
        StartCoroutine(LoadLevelCO());
    }

    private IEnumerator LoadLevelCO()
    {
        _lSUICRef.FadeToBlack();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_lSPRef.currentPoint.levelToLoad);
    }
}
