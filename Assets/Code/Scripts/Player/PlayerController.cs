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
    private bool _isGrounded;

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
        //Si el contador de knockback se ha vaciado, el jugador recupera el control
        if (_knockbackCounter <= 0 && _wallJumpCounter <= 0 && !_isDashing)
        {
            //Movimiento
            _playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, _playerRB.velocity.y);

            //�Est� en el suelo?
            _isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

            //�Est� tocando la pared?
            _isWalledLeft = Physics2D.OverlapCircle(wallPointLeft.position, 0.2f, whatIsGround);

            _isWalledRight = Physics2D.OverlapCircle(wallPointRight.position, 0.2f, whatIsGround);

            //Salto
            if (Input.GetButtonDown("Jump") && _isGrounded)
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
            if (Input.GetButtonDown("Jump") && _isWalledRight && !_isGrounded)
            {
                _wallJumpCounter = wallJumpCounterLength;
                _playerRB.velocity = new Vector2(-0.9f * playerJumpForce, 0.9f * playerJumpForce);
                jumpNumber = 0;
            }
            if (Input.GetButtonDown("Jump") && _isWalledLeft && !_isGrounded)
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

            //Cambio de direcci�n del sprite
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

                if (!_playerSpriteRenderer.flipX)
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
        if (_isGrounded)
        {
            jumpNumber = 0;
        }

        //Animaciones
        _anim.SetBool("isGrounded", _isGrounded);

        //Math.Abs devuelve el absoluto de una variable
        _anim.SetFloat("MoveSpeed", Mathf.Abs(_playerRB.velocity.x));
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

        if(_isWalledLeft)
            _playerRB.velocity = new Vector2(1.5f * dashSpeed, 0f);
        else if(_isWalledRight)
            _playerRB.velocity = new Vector2(-1.5f * dashSpeed, 0f);
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
