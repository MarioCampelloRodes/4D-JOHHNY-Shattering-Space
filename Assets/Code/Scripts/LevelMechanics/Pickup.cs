using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isPoint, isRestoreHealth;

    private bool _isCollected;
    private UIController _uIRef;
    private PlayerHealthController _pHCRef;
    // Start is called before the first frame update
    void Start()
    {
        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
        _pHCRef = GameObject.Find("Player").GetComponent<PlayerHealthController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isCollected)
        {
            if (isPoint)
            {
                _uIRef.AddScore(100);

                _isCollected = true;

                Destroy(gameObject);
            }

            if (isRestoreHealth)
            {
                if(_pHCRef.currentHealth <= _pHCRef.maxHealth -1)
                {
                    _pHCRef.currentHealth++;

                    _uIRef.UpdateHealth();

                    _isCollected = true;

                    Destroy(gameObject);
                }
            }
        }
    }
}
