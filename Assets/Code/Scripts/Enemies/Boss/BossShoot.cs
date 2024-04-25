using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public float moveSpeed;
    public int damageDealt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-moveSpeed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona de la bala
        if (collision.CompareTag("Player"))
        {
            //El jugador recibirá daño
            collision.GetComponent<PlayerHealthController>().DealWithDamage(damageDealt);
            //Destruimos la bala
            Destroy(gameObject);
        }

    }
}
