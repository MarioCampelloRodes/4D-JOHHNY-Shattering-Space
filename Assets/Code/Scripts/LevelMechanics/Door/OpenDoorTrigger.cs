using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    public Door doorLeftRef, doorRightRef;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doorLeftRef.DeactivateObject();
            doorRightRef.DeactivateObject();
        }
    }
}
