using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnnyController : MonoBehaviour
{
    //Movimiento Default
    public float playerSpeed;
    public bool canMove = true;
    
    //Saltos
    public float playerJumpForce;
    public bool isGrounded;
    public float fallSpeed;

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

    //Puntos para detectar pared/suelo
    public Transform groundPoint;

    //Detector de capas
    public LayerMask whatIsGround;

    //Diálogos
    public bool isInDialogue;

    //Referencias
    private Rigidbody2D _playerRB;
    private SpriteRenderer _playerSpriteRenderer;
    private Animator _anim;
    private LevelOnePHC _pHCRef;

    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();

        _anim = GetComponent<Animator>();

        _playerSpriteRenderer = GetComponent<SpriteRenderer>();

        _pHCRef = GetComponent<LevelOnePHC>();
    }

    // Update is called once per frame
    void Update()
    {
        //¿Está en el suelo?
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        //Si el contador de knockback se ha vaciado, el jugador recupera el control
        if (_knockbackCounter <= 0 && !_isDashing && _pHCRef.currentHealth >= 0 && canMove && !isInDialogue)
        {
            //Movimiento
            if (isGrounded && !_isDashing)
                _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, _playerRB.velocity.y);
            else if ((Input.GetAxisRaw("Horizontal") > 0.1f && _playerRB.velocity.x <= -dashSpeed *0.95) || (Input.GetAxisRaw("Horizontal") < -0.1f && _playerRB.velocity.x >= dashSpeed *0.95))
                _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, _playerRB.velocity.y);
            else if (_playerRB.velocity.x > -dashSpeed * 0.95 && _playerRB.velocity.x < dashSpeed * 0.95)
                _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, _playerRB.velocity.y);
            
            if (!isGrounded && Input.GetAxisRaw("Vertical") <= -0.1f)
                _playerRB.velocity = new Vector2(0f, _playerRB.velocity.y - 0.1f);

            //Salto
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, playerJumpForce);
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
        }

        //Límite Velocidad de Caída
        if(_playerRB.velocity.y < fallSpeed)
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, fallSpeed);
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

        if (_playerSpriteRenderer.flipX)
            _playerRB.velocity = new Vector2(dashSpeed, 0f);
        if (!_playerSpriteRenderer.flipX)
            _playerRB.velocity = new Vector2(-dashSpeed, 0f);

        _pHCRef.invincibleCounter = dashTime;
        _anim.SetTrigger("IsDashing");

        yield return new WaitForSeconds(dashTime);

        _isDashing = false;
        _playerRB.gravityScale = rbGravity;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}