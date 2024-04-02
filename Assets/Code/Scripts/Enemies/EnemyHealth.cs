using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool isDamaged;

    SpriteRenderer _sPR;

    private void Start()
    {
        currentHealth = maxHealth;

        _sPR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (currentHealth <= 0) 
        {
            EnemyDeathController();
        }
    }
    public GameObject deathEffect;
    // Start is called before the first frame update
    
    void EnemyDeathController()
    {
        Instantiate(deathEffect, transform.GetChild(0).position, transform.GetChild(0).rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(TakeDamageCO(damage));
    }

    private IEnumerator TakeDamageCO(int damage)
    {
        currentHealth -= damage;

        _sPR.color = new Color(_sPR.color.r, _sPR.color.g, _sPR.color.b, 0.7f);

        isDamaged = true;

        yield return new WaitForSeconds(0.5f);

        _sPR.color = Color.white;

        isDamaged = false;
    }
}
