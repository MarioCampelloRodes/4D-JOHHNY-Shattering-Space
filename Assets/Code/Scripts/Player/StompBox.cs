using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    private IkalController _iCRef;

    public float bounceForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _iCRef = GetComponentInParent<IkalController>();
         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponentInParent<EnemyDeath>().EnemyDeathController();
            _iCRef.Bounce(bounceForce);
        }
    }
}