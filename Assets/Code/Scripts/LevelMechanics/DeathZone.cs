using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private LevelManager _lMRef;

    private void Start()
    {
        _lMRef = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _lMRef.RespawnPlayer();
        }
    }
}
