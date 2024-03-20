using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    private PlayerController _pCRef;

    public float bounceForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _pCRef = GetComponentInParent<PlayerController>();
         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponentInParent<EnemyDeath>().EnemyDeathController();
            _pCRef.Bounce(bounceForce);
        }
    }
}