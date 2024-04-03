using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool isClosed;

    public GameObject lastWaveEnemy;
    public Door doorLeftRef, doorRightRef;


    private void Update()
    {
        if(lastWaveEnemy == null && isClosed)
        {
            doorLeftRef.DeactivateObject();
            doorRightRef.DeactivateObject();
            isClosed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorLeftRef.ActivateObject();
            doorRightRef.ActivateObject();
            isClosed = true;
        }
    }
}
