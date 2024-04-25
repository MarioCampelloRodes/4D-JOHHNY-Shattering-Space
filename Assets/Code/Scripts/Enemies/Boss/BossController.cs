using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //Lista de estados por los que puede pasar el jefe final (Máquina de estados)
    public enum bossStates { shooting, pursuit, stayStill, defeated };

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

    //Tiempo de spawnear enemigos 4D
    public float spawnCooldown;
    private float _spawnCounter;

    [Header("References")]
    //Posición del Boos
    public Transform bossPosition;
    //Referencia al Animator del jefe final
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
            //En el caso en el que currentState = 0
            case bossStates.shooting:
                //Hacemos decrecer el contador entre disparos
                _shotCounter -= Time.deltaTime;

                //Si el contador de tiempo entre disparos se ha vaciado
                if (_shotCounter <= 0)
                {
                    //Reiniciamos el contador de tiempo entre disparos
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
                    //Hacemos decrecer el contador de tiempo de daño
                    _stateChangeCounter -= Time.deltaTime;

                    //Si el contador de tiempo de daño se ha vaciado
                    if (_stateChangeCounter <= 0)
                    {
                        _stateChangeCounter = stateChangeTime;
                        currentState = bossStates.pursuit;
                    }
                }
                break;

            case bossStates.stayStill:
                //Si el contador de tiempo de daño aún no está vacío
                if (_stateChangeCounter > 0)
                {
                    //Hacemos decrecer el contador de tiempo de daño
                    _stateChangeCounter -= Time.deltaTime;

                    //Si el contador de tiempo de daño se ha vaciado
                    if (_stateChangeCounter <= 0)
                    {
                        _stateChangeCounter = stateChangeTime; 
                        currentState = bossStates.shooting;
                    }
                }
                break;
            //En el caso en el que currentState = 2
            case bossStates.pursuit:
                if (_stateChangeCounter > 0)
                {
                    //Hacemos decrecer el contador de tiempo de daño
                    _stateChangeCounter -= Time.deltaTime;

                    if (_stateChangeCounter <= 0)
                    {
                        _stateChangeCounter = stateChangeTime;
                        RandomShootPoint();
                        currentState = bossStates.shooting;
                    }
                }
                break;
            //En el caso en el que currentState = 3
            case bossStates.defeated:
                Debug.Log("Ended");
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
}