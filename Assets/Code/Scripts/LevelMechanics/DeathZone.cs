using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private PlayerHealthController _pCHRef;

    private void Start()
    {
        _pCHRef = GameObject.Find("Player").GetComponent<PlayerHealthController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Caíste");
            _pCHRef.currentHealth = 0;
            _pCHRef.DealWithDamage();
        }
    }
}
