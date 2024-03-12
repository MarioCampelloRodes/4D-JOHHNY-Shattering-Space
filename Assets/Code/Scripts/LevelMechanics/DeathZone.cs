using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private LevelManager _lMRef;
    private PlayerHealthController _pHCRef;
    private UIController _uIRef;

    private void Start()
    {
        _lMRef = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _pHCRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _pHCRef.currentHealth = 0;
            _uIRef.UpdateHealth();
            _lMRef.RespawnPlayer();
        }
    }
}
