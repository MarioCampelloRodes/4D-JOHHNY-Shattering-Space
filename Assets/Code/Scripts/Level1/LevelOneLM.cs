using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneLM : MonoBehaviour
{
    public float timeForRespawn = 2f;
    public bool isPursuitActive;

    private JohnnyController _jCRef;
    private CheckpointController _cpRef;
    private UIController _uIRef;
    private LevelOnePHC _pHCRef;

    private void Start()
    {
        _jCRef = GameObject.FindGameObjectWithTag("Player").GetComponent<JohnnyController>();

        _cpRef = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();

        _pHCRef = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelOnePHC>();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCO());
    }

    private IEnumerator RespawnPlayerCO()
    {
        _jCRef.gameObject.SetActive(false);

        yield return new WaitForSeconds(timeForRespawn);

        _jCRef.gameObject.SetActive(true);
        _jCRef.gameObject.transform.position = _cpRef.spawnPoint;
        _jCRef.canDash = true;
        _pHCRef.currentHealth = _pHCRef.maxHealth;

        if(isPursuitActive) 
        {
            GameObject.Find("Pursuit(Clone)").transform.position = _cpRef.spawnPoint;
        }

        AudioManager.aMRef.PlaySFX(17);
    }
}
