using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimiento Default
    public float playerSpeed;
    
    //Saltos
    public float playerJumpForce;
    public int jumpNumber = 0;
    public bool isGrounded;

    //Saltos de Pared
    private bool _isWalledLeft, _isWalledRight;
    public float wallJumpCounterLength;
    private float _wallJumpCounter;

    //Dash
    private bool _isDashing;
    public bool canDash = true;
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;

    //Knockbacks
    public float knockbackForce = 3f;
    public float knockbackCounterLength;
    private float _knockbackCounter;
    private float _enemyXPos;

    //Boost
    public float boostSpeed;
    public float boostTimeLength;
    public float boostTime;


    //Puntos para detectar pared/suelo
    public Transform groundPoint;
    public Transform wallPointLeft, wallPointRight;

    //Detector de capas
    public LayerMask whatIsGround;

    //Referencias
    private Rigidbody2D _playerRB;
    private SpriteRenderer _playerSpriteRenderer;
    private Animator _anim;
    private PlayerHealthController _pHCRef;

    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();

        _anim = GetComponent<Animator>();

        _playerSpriteRenderer = GetComponent<SpriteRenderer>();

        _pHCRef = GetComponent<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        //¿Está en el suelo?
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        //Si el contador de knockback se ha vaciado, el jugador recupera el control
        if (_knockbackCounter <= 0 && _wallJumpCounter <= 0 && !_isDashing && _pHCRef.currentHealth >= 0)
        {
            //Movimiento
            if (boostTime > 0) //Movimiento Boost
            {
                if (isGrounded)
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * boostSpeed, _playerRB.velocity.y);
                else if (Input.GetAxisRaw("Horizontal") > 0.1f || Input.GetAxisRaw("Horizontal") < -0.1f)
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * boostSpeed, _playerRB.velocity.y);
            }
            else //Movimiento Default
            {
                if (isGrounded)
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, _playerRB.velocity.y);
                else if (Input.GetAxisRaw("Horizontal") > 0.1f || Input.GetAxisRaw("Horizontal") < -0.1f)
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, _playerRB.velocity.y);
            }
            
            //¿Está tocando la pared?
            _isWalledLeft = Physics2D.OverlapCircle(wallPointLeft.position, 0.2f, whatIsGround);

            _isWalledRight = Physics2D.OverlapCircle(wallPointRight.position, 0.2f, whatIsGround);

            //Salto
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, playerJumpForce);
            }
            //Doble Salto
            else if (Input.GetButtonDown("Jump") && jumpNumber == 0)
            {
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, playerJumpForce);
                jumpNumber++;
            }
            //Salto de Pared
            if (Input.GetButtonDown("Jump") && _isWalledRight && !isGrounded)
            {
                _wallJumpCounter = wallJumpCounterLength;
                _playerRB.velocity = new Vector2(-0.9f * playerJumpForce, 0.9f * playerJumpForce);
                jumpNumber = 0;
            }
            if (Input.GetButtonDown("Jump") && _isWalledLeft && !isGrounded)
            {
                _wallJumpCounter = wallJumpCounterLength;
                _playerRB.velocity = new Vector2(0.9f * playerJumpForce, 0.9f * playerJumpForce);
                jumpNumber = 0;
            }

            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                Dash();
            }

            //Cambio de dirección del sprite
            if (_playerRB.velocity.x < 0)
            {
                _playerSpriteRenderer.flipX = false;
            }
            else if (_playerRB.velocity.x > 0)
            {
                _playerSpriteRenderer.flipX = true;
            }
        }
        else
        {
            if(_knockbackCounter > 0)
            {
                _knockbackCounter -= Time.deltaTime;

                if (transform.position.x > _enemyXPos)
                {
                    _playerRB.velocity = new Vector2(knockbackForce, _playerRB.velocity.y);
                }
                else
                {
                    _playerRB.velocity = new Vector2(-knockbackForce, _playerRB.velocity.y);
                }
            }

            if (_wallJumpCounter > 0)
            {
                _wallJumpCounter -= Time.deltaTime;
            }
        }
        //Reseteo de Saltos
        if (isGrounded)
        {
            jumpNumber = 0;
        }
        //Contador de boost
        if(boostTime > 0)
        {
            boostTime -= Time.deltaTime;
        }
        //Animaciones
        _anim.SetBool("isGrounded", isGrounded);

        //Math.Abs devuelve el absoluto de una variable
        _anim.SetFloat("MoveSpeed", Mathf.Abs(_playerRB.velocity.x));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemyXPos = collision.transform.position.x;
        }
    }
    public void Knockback()
    {
        _knockbackCounter = knockbackCounterLength;

        _playerRB.velocity = new Vector2(0f, knockbackForce);
        _anim.SetTrigger("IsHurt");
    }

    void Dash()
    {
        StartCoroutine("DashCO");
    }

    private IEnumerator DashCO()
    {
        _isDashing = true;
        canDash = false;
        float rbGravity = _playerRB.gravityScale;
        _playerRB.gravityScale = 0f;

        if (_isWalledLeft && !isGrounded)
        {
            _playerRB.velocity = new Vector2(dashSpeed, 0f);
            jumpNumber = 0;
        }
            
        else if (_isWalledRight && !isGrounded)
        {
            _playerRB.velocity = new Vector2(-dashSpeed, 0f);
            jumpNumber = 0;
        }
            
        else
        {
            if (_playerSpriteRenderer.flipX)
                _playerRB.velocity = new Vector2(dashSpeed, 0f);
            if (!_playerSpriteRenderer.flipX)
                _playerRB.velocity = new Vector2(-dashSpeed, 0f);
        }

        _pHCRef.invincibleCounter = dashTime;
        _anim.SetTrigger("IsDashing");

        yield return new WaitForSeconds(dashTime);

        _isDashing = false;
        _playerRB.gravityScale = rbGravity;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}