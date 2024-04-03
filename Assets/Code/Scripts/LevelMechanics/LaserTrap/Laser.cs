using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed = 3f;

    private Rigidbody2D _rB;

    // Start is called before the first frame update
    void Start()
    {
        _rB = GetComponent<Rigidbody2D>();

        _rB.velocity = new Vector2(-laserSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}