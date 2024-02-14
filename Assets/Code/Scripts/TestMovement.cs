using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float playerSpeed = 5f;

    public float playerJumpForce = 10f;

    private int _jumpNumber = 0;
    
    Rigidbody2D _playerRB;

    private bool _isGrounded;

    public Transform groundCheckPoint;

    public LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, _playerRB.velocity.y);

        _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, playerJumpForce);
        }
        else if (Input.GetButtonDown("Jump") && _jumpNumber < 1)
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, playerJumpForce);
            _jumpNumber++;
        }
        else if (_isGrounded)
        {
            _jumpNumber = 0;
        }
    }

    
}
