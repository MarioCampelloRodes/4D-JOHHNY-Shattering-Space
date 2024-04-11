using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDPlayer : MonoBehaviour
{
    public GameObject bullet;

    public float shootCooldownLength;
    private float _shootCooldown;

    public LayerMask whatIs4D;

    public bool shootingLeft, shootingRight, shootingTop, shootingBottom;

    public float dangerRadius;
    public GameObject dangerSignScreen, dangerSignRadar;
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

        Collider2D[] nearEnemies = Physics2D.OverlapCircleAll(transform.position, dangerRadius, whatIs4D);

        foreach (Collider2D enemy in nearEnemies)
        {

            if (!dangerSignScreen.activeSelf && !dangerSignRadar.activeSelf)
            {
                dangerSignRadar.SetActive(true);
                dangerSignScreen.SetActive(true);
            }
        }

        if(nearEnemies.Length == 0)
        {
            dangerSignRadar.SetActive(false);
            dangerSignScreen.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dangerRadius);
    }
}
