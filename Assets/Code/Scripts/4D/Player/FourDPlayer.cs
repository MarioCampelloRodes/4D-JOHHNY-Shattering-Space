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
    public GameObject dangerSignScreen, dangerSignRadar, dangerEffect;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("DPadX") < 0f || Input.GetAxis("RightStickX") < -0.01f) && _shootCooldown <= 0)
        {
            shootingLeft = true;
            shootingRight = false;
            shootingTop = false;
            shootingBottom = false;
            Instantiate(bullet, transform.position + new Vector3(-0.4f, -0.075f, 0f), transform.rotation);
            _shootCooldown = shootCooldownLength;

            AudioManager.aMRef.PlaySFX(1);
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("DPadX") > 0f || Input.GetAxis("RightStickX") > 0.01f) && _shootCooldown <= 0)
        {
            shootingLeft = false;
            shootingRight = true;
            shootingTop = false;
            shootingBottom = false;
            Instantiate(bullet, transform.position + new Vector3(0.4f, -0.075f, 0f), transform.rotation);
            _shootCooldown = shootCooldownLength;

            AudioManager.aMRef.PlaySFX(1);
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("DPadY") > 0f || Input.GetAxis("RightStickY") > 0.01f) && _shootCooldown <= 0)
        {
            shootingLeft = false;
            shootingRight = false;
            shootingTop = true;
            shootingBottom = false;
            Instantiate(bullet, transform.position + new Vector3(0f, 0.4f, 0f), transform.rotation);
            _shootCooldown = shootCooldownLength;

            AudioManager.aMRef.PlaySFX(1);
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("DPadY") < 0f || Input.GetAxis("RightStickY") < -0.01f) && _shootCooldown <= 0)
        {
            shootingLeft = false;
            shootingRight = false;
            shootingTop = false;
            shootingBottom = true;
            Instantiate(bullet, transform.position + new Vector3(0, -0.4f, 0f), transform.rotation);
            _shootCooldown = shootCooldownLength;

            AudioManager.aMRef.PlaySFX(1);
        }

        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }

        //Detección de Peligros cercanos
        Collider2D[] nearEnemies = Physics2D.OverlapCircleAll(transform.position, dangerRadius, whatIs4D);

        foreach (Collider2D enemy in nearEnemies)
        {

            if (!dangerSignScreen.activeSelf && !dangerSignRadar.activeSelf)
            {
                dangerSignRadar.SetActive(true);
                dangerSignScreen.SetActive(true);
                dangerEffect.SetActive(true);

                AudioManager.aMRef.PlaySFX(0);
            }
        }

        if (nearEnemies.Length == 0)
        {
            dangerSignRadar.SetActive(false);
            dangerSignScreen.SetActive(false);
            dangerEffect.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dangerRadius);
    }
}
