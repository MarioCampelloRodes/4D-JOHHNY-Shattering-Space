using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeBoxLeft : MonoBehaviour
{
    private SlimeController _sCRef;

    private void Start()
    {
        _sCRef = GetComponentInParent<SlimeController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _sCRef.seeingLeft = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _sCRef.seeingLeft = false;
        }
    }
}
