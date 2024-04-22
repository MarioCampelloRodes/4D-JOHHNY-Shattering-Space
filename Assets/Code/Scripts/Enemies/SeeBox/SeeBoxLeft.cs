using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeBoxLeft : MonoBehaviour
{
    private EnemyController _sCRef;
    public bool isWolf;

    private void Start()
    {
        _sCRef = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _sCRef.seeingLeft = true;

            if(!_sCRef.movingRight)
            {
                if (isWolf)
                {
                    AudioManager.aMRef.PlaySFX(19);
                }
                else
                {
                    AudioManager.aMRef.PlaySFX(11);
                }
            }
            
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
