using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    Rigidbody2D _playerRB;
    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, _playerRB.velocity.y);
    }

    
}
