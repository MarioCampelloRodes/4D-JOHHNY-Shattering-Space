using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneDamagePlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<LevelOnePHC>().DealWithDamage();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<LevelOnePHC>().DealWithDamage();
        }
    }
}
