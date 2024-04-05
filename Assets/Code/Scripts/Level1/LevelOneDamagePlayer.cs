using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneDamagePlayer : MonoBehaviour
{
    public int damageDealt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<LevelOnePHC>().DealWithDamage(damageDealt);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<LevelOnePHC>().DealWithDamage(damageDealt);
        }
    }
}
