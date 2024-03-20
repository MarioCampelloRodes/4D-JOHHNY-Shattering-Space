using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float timeForRespawn = 2f;

    private IkalController _iCRef;
    private CheckpointController _cpRef;
    private UIController _uIRef;
    private PlayerHealthController _pHCRef;

    private void Start()
    {
        _iCRef = GameObject.FindGameObjectWithTag("Player").GetComponent<IkalController>();

        _cpRef = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
        
        _pHCRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();

        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCO());
    }

    private IEnumerator RespawnPlayerCO()
    {
        _iCRef.gameObject.SetActive(false);

        yield return new WaitForSeconds(timeForRespawn);

        _iCRef.gameObject.SetActive(true);
        _iCRef.gameObject.transform.position = _cpRef.spawnPoint;
        _iCRef.canDash = true;
        _pHCRef.currentHealth = _pHCRef.maxHealth;
        _uIRef.UpdateHealth();

    }
}
