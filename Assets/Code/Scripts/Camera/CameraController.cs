using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    //Referencias a las posiciones de los fondos
    //public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;

    //Última posición del jugador en X y en Y
    private Vector2 _lastPos;
    // Start is called before the first frame update
    void Start()
    {
        _lastPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        ////Variable para conocer cuanto hay que moverse en X y en Y
        //Vector2 _amountToMove = new Vector2(target.position.x - _lastPos.x, target.position.y - _lastPos.y);


        //Restricción entre un mínimo y un máximo para la cámara en y
        transform.position = new Vector3(target.position.x - 2f, Mathf.Clamp(transform.position.y, minHeight, maxHeight) + 1f, transform.position.z);

        //farBackground.position += new Vector3(_amountToMove.x, _amountToMove.y, 0f);
        //middleBackground.position += new Vector3(_amountToMove.x * 0.5f, _amountToMove.y * -0.1f, 0f);

        //Actualizamos la posición del jugador
        _lastPos.x = target.position.x;
        _lastPos.y = target.position.y;
    }
}
