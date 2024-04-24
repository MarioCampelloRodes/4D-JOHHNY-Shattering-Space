using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //Lista de estados por los que puede pasar el jefe final (Máquina de estados)
    public enum bossStates { shooting, pursuit, stayStill, defeated };

    public bossStates currentState;

    public Transform leftShootPoint, rightShootPoint, leftPoint, topPoint, rightPoint, downPoint;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Shooting")]

    public float shootCooldown;
    private float _shotCounter;

    public GameObject bulletLeft, bulletRight;
    public Transform firePointTopLeft, firePointMiddleLeft, firePointDownLeft, firePointTopRight, firePointMiddleRight, firePointDownRight;

    [Header("Stay Still")]
    //Tiempo de daño del enemigo
    public float stayStillTime;
    //Contador de tiempo de daño
    private float _stayStillCounter;

    [Header("References")]
    //Posición del Boos
    public Transform theBoss;
    //Referencia al Animator del jefe final
    private Animator _bAnim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = bossStates.stayStill;

        _bAnim = GetComponentInChildren<Animator>();
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
                    //Instanciamos la bala pero en una nueva referencia cada vez
                    GameObject b = Instantiate(bulletLeft, firePointTopLeft.position, firePointTopLeft.rotation);
                    //Como cada bala estará referenciada (será única) puedo aplicarle los cambios que queramos
                    //En este caso le diré a cada bala hacia donde debe apuntar según hacia donde mira el jefe final
                    b.transform.localScale = theBoss.localScale;
                }
                break;
            //En el caso en el que currentState = 1
            case bossStates.stayStill:
                //Si el contador de tiempo de daño aún no está vacío
                if (_stayStillCounter > 0)
                {
                    //Hacemos decrecer el contador de tiempo de daño
                    _stayStillCounter -= Time.deltaTime;

                    //Si el contador de tiempo de daño se ha vaciado
                    if (_stayStillCounter <= 0)
                    {
                        //El jefe final pasaría al estado de movimiento
                        currentState = bossStates.shooting;
                    }
                }
                break;
            //En el caso en el que currentState = 2
            case bossStates.pursuit:
                //Si el enemigo se mueve a la derecha
                
                break;
            //En el caso en el que currentState = 3
            case bossStates.defeated:
                Debug.Log("Ended");
                break;
        }
    }
}