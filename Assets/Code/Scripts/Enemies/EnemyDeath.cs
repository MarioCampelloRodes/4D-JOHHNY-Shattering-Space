using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject deathEffect;
    // Start is called before the first frame update
    
    public void EnemyDeathController()
    {
        transform.gameObject.SetActive(false);
        Instantiate(deathEffect, transform.GetChild(0).position, transform.GetChild(0).rotation);
    }
}
