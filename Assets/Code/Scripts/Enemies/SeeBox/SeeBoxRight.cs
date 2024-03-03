using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeBoxRight : MonoBehaviour
{
    private SlimeController _sCRef;

    private void Start()
    {
        _sCRef = GetComponentInParent<SlimeController>();

        Debug.Log("Mewing");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _sCRef.seeingRight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _sCRef.seeingRight = false;
        }
    }
}
