using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public bool leftDirection;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leftDirection)
        {
            rb.velocity = new Vector2(-speed, 0f);
        }

        if (!leftDirection)
        {
            rb.velocity = new Vector2(speed, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(gameObject);

            AudioManager.aMRef.PlaySFX(1);
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            AudioManager.aMRef.PlaySFX(1);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        
    }
}
