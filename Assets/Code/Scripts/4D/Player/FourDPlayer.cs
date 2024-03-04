using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDPlayer : MonoBehaviour
{
    public GameObject bullet;

    public float shootCooldownLength;
    private float _shootCooldown;

    public bool shootingLeft, shootingRight, shootingTop, shootingBottom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && _shootCooldown <= 0) 
        {
            shootingLeft = true;
            shootingRight = false;
            shootingTop = false;
            shootingBottom = false;
            Instantiate(bullet, transform.position + new Vector3(-0.4f, -0.075f, 0f), transform.rotation);
            _shootCooldown = shootCooldownLength;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && _shootCooldown <= 0)
        {
            shootingLeft = false;
            shootingRight = true;
            shootingTop = false;
            shootingBottom = false;
            Instantiate(bullet, transform.position + new Vector3(0.4f, -0.075f, 0f), transform.rotation);
            _shootCooldown = shootCooldownLength;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && _shootCooldown <= 0)
        {
            shootingLeft = false;
            shootingRight = false;
            shootingTop = true;
            shootingBottom = false;
            Instantiate(bullet, transform.position + new Vector3(0f, 0.4f, 0f), transform.rotation);
            _shootCooldown = shootCooldownLength;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && _shootCooldown <= 0)
        {
            shootingLeft = false;
            shootingRight = false;
            shootingTop = false;
            shootingBottom = true;
            Instantiate(bullet, transform.position + new Vector3(0, -0.4f, 0f), transform.rotation);
            _shootCooldown = shootCooldownLength;
        }

        if (_shootCooldown > 0) 
        { 
            _shootCooldown -= Time.deltaTime;
        }
    }
}
