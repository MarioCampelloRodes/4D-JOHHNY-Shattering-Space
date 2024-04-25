using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //Lista de estados por los que puede pasar el jefe final (Máquina de estados)
    public enum bossStates { shooting, pursuit, stayStill};

    public bossStates currentState;

    public Transform leftShootPoint, rightShootPoint, topPoint, downPoint, disappearPoint;

    public float stateChangeTime;
    private float _stateChangeCounter;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Shooting")]
    private int _shootingStateNumber;
    private int _shootAmountNumber;

    public float shootCooldown;
    private float _shotCounter;

    public GameObject bulletPrefab;
    public Transform firePointTopLeft, firePointMiddleLeft, firePointDownLeft, firePointTopRight, firePointMiddleRight, firePointDownRight;

    [Header("Pursuit")]

    public GameObject pursuitAttackPrefab;

    //Tiempo entre ataques de persecución
    public float pursuitCooldown;
    private float _pursuitCounter;

    [Header("Stay Still")]
    private int _stayingStateNumber;
    //Tiempo de spawnear enemigos 4D
    public float spawnCooldown;
    private float _spawnCounter;

    [Header("References")]

    public Transform bossPosition;

    public EnemySpawner _eSRef;

    private Animator _bAnim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = bossStates.stayStill;

        _bAnim = GetComponentInChildren<Animator>();

        _stateChangeCounter = stateChangeTime;

        Invoke("RandomShootPoint", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //MÁQUINA DE ESTADOS
        //En base a los cambios en el valor de enum generamos los cambios de estado
        switch (currentState)
        {
            case bossStates.shooting:
                _shotCounter -= Time.deltaTime;

                if (_shotCounter <= 0)
                {
                    _shotCounter = shootCooldown;

                    _shootAmountNumber++;

                    if (_shootingStateNumber % 2 == 0)
                    {
                        if (_shootAmountNumber % 2 == 0)
                        {
                            GameObject topRightBullet = Instantiate(bulletPrefab, firePointTopRight.position, firePointTopRight.rotation);

                            topRightBullet.transform.localScale = bossPosition.localScale;

                            GameObject downRightBullet = Instantiate(bulletPrefab, firePointDownRight.position, firePointDownRight.rotation);

                            downRightBullet.transform.localScale = bossPosition.localScale;
                        }
                        else
                        {
                            GameObject middleRightBullet = Instantiate(bulletPrefab, firePointMiddleRight.position, firePointMiddleRight.rotation);

                            middleRightBullet.transform.localScale = bossPosition.localScale;
                        }
                    }
                    else
                    {
                        if (_shootAmountNumber % 2 == 0)
                        {
                            GameObject topLeftBullet = Instantiate(bulletPrefab, firePointTopLeft.position, firePointTopLeft.rotation);

                            topLeftBullet.transform.localScale = bossPosition.localScale;

                            GameObject downLeftBullet = Instantiate(bulletPrefab, firePointDownLeft.position, firePointDownLeft.rotation);

                            downLeftBullet.transform.localScale = bossPosition.localScale;
                        }
                        else
                        {
                            GameObject middleLeftBullet = Instantiate(bulletPrefab, firePointMiddleLeft.position, firePointMiddleLeft.rotation);

                            middleLeftBullet.transform.localScale = bossPosition.localScale;
                        }
                    }
                }

                if (_stateChangeCounter > 0)
                {
                    _stateChangeCounter -= Time.deltaTime;

                    if (_stateChangeCounter <= 0)
                    {
                        _stateChangeCounter = stateChangeTime;
                        bossPosition.position = disappearPoint.position;
                        currentState = bossStates.pursuit;
                    }
                }
                break;

            case bossStates.stayStill:
                if(_spawnCounter > 0)
                {
                    _spawnCounter -= Time.deltaTime;

                    if (_spawnCounter <= 0)
                    {
                        _eSRef.SpawnEnemy();
                        _spawnCounter = spawnCooldown;
                    }
                }
                if (_stateChangeCounter > 0)
                {
                    _stateChangeCounter -= Time.deltaTime;

                    if (_stateChangeCounter <= 0)
                    {
                        _stateChangeCounter = stateChangeTime;
                        RandomShootPoint();
                    }
                }
                break;

            case bossStates.pursuit:
                if (_stateChangeCounter > 0)
                {
                    _stateChangeCounter -= Time.deltaTime;

                    if (_stateChangeCounter <= 0)
                    {
                        _stateChangeCounter = stateChangeTime;
                        _spawnCounter = spawnCooldown;
                        RandomStayPoint();
                    }
                }
                break;
        }
    }

    void RandomShootPoint()
    {
        _shootingStateNumber++;

        if(_shootingStateNumber % 2 == 0)
        {
            bossPosition.position = rightShootPoint.position;
            bossPosition.localScale = Vector3.one;
        }
        else
        {
            bossPosition.position = leftShootPoint.position;
            bossPosition.localScale = new Vector3(-1, 1, 1);
        }

        currentState = bossStates.shooting;
    }

    void RandomStayPoint()
    {
        _stayingStateNumber++;

        if (_stayingStateNumber % 2 == 0)
        {
            bossPosition.position = downPoint.position;
            bossPosition.localScale = Vector3.one;
        }
        else
        {
            bossPosition.position = topPoint.position;
            bossPosition.localScale = Vector3.one;
        }

        currentState = bossStates.stayStill;
    }
}