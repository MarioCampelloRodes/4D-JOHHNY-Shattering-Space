using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneSlowPursuit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindWithTag("Pursuit").GetComponent<LevelOnePursuit>().speed *= 0.75f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindWithTag("Pursuit").GetComponent<LevelOnePursuit>().speed *= 1.33f;
        }
    }
}
