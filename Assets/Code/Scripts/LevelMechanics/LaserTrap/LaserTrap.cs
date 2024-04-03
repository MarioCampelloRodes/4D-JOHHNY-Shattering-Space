using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laserPrefab;

    public float shootCounterLength;
    private float shootCounter;

    // Start is called before the first frame update
    void Start()
    {
        shootCounter = shootCounterLength;
    }

    // Update is called once per frame
    void Update()
    {
        shootCounter -= Time.deltaTime;

        if(shootCounter <= 0) 
        { 
            Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

            shootCounter = shootCounterLength;
        }
    }
}
