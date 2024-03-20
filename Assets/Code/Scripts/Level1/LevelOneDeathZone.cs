using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneDeathZone : MonoBehaviour
{
    private LevelOneLM _lMRef;
    private LevelOnePHC _pHCRef;

    private void Start()
    {
        _lMRef = GameObject.Find("L1LevelManager").GetComponent<LevelOneLM>();
        _pHCRef = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelOnePHC>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _pHCRef.currentHealth = 0;
            _lMRef.RespawnPlayer();
        }
    }
}
