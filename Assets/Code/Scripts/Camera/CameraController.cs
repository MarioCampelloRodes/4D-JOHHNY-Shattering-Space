using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public bool isFreezed;
    public float xOffSet = -2f, yOffSet = 1f;

    //Referencias a las posiciones de los fondos
    public Transform veryFarBackground, farBackground, middleBackground, nearBackGround;
    public float minHeight, maxHeight, minX, maxX;

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
        if(!isFreezed)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

            ////Variable para conocer cuanto hay que moverse en X y en Y
            Vector2 _amountToMove = new Vector2(target.position.x - _lastPos.x, target.position.y - _lastPos.y);


            //Restricción entre un mínimo y un máximo para la cámara en y
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX) + xOffSet, Mathf.Clamp(transform.position.y, minHeight, maxHeight) + yOffSet, transform.position.z);

            veryFarBackground.position += new Vector3(_amountToMove.x * 0.95f, _amountToMove.y, 0f);
            farBackground.position += new Vector3(_amountToMove.x * 0.85f, _amountToMove.y * 0.9f, 0f);
            middleBackground.position += new Vector3(_amountToMove.x * 0.6f, _amountToMove.y * -0.005f, 0f);
            nearBackGround.position += new Vector3(_amountToMove.x * 0.2f, _amountToMove.y * -0.0075f, 0f);

            //Actualizamos la posición del jugador
            _lastPos.x = target.position.x;
            _lastPos.y = target.position.y;
        }
    }
}
