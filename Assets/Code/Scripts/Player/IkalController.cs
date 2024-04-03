using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkalController : MonoBehaviour
{
    //�Tiene el control?
    private bool _canMove;
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

    //Ataque
    public Transform attackPointLeft, attackPointRight;
    public int lightDamage = 2, heavyDamage = 4;
    public float attackRange = 1f;
    public float attackCounterLength = 0.25f;
    public float heavyAttackCounterLength = 0.4f;
    private float attackCounter;
    public float heavyAttackHoldLength = 1f;
    private float attackHoldTime;
    private bool canHeavyAttack = true;

    //Knockbacks
    public float knockbackForce = 3f;
    public float knockbackCounterLength;
    private float _knockbackCounter;
    private float _enemyXPos;

    //Boost
    public float boostSpeed;
    public float boostTimeLength;
    public float boostTime;

    //Interactuable
    public bool canInteract;

    //Puntos para detectar pared/suelo
    public Transform groundPoint;
    public Transform wallPointLeft, wallPointRight;

    //Detectores de capas
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;

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
        //�Est� en el suelo?
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        //�Tiene el control?
        if (_knockbackCounter <= 0 && _wallJumpCounter <= 0 && !_isDashing && _pHCRef.currentHealth >= 0)
        {
            _canMove = true;
        }
        else
        {
            _canMove = false;
        }
        //Si los contadores se han vaciado, el jugador recupera el control
        if (_canMove)
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

            //�Est� tocando la pared?
            Collider2D rightWall = Physics2D.OverlapCircle(wallPointRight.position, 0.2f, whatIsGround);
            if(rightWall != null && !rightWall.usedByEffector)
            {
                _isWalledRight = true;
            }
            else
            {
                _isWalledRight = false;
            }

            Collider2D leftWall = Physics2D.OverlapCircle(wallPointLeft.position, 0.2f, whatIsGround);
            if (leftWall != null && !leftWall.usedByEffector)
            {
                _isWalledLeft = true;
            }
            else
            {
                _isWalledLeft = false;
            }

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

            //Ataque
            if(attackCounter <= 0)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    attackHoldTime += Time.deltaTime;
                    
                    //Ataque Pesado
                    if (attackHoldTime > heavyAttackHoldLength && canHeavyAttack)
                    {
                        HeavyAttack();
                        canHeavyAttack = false;
                    }
                }
                if (Input.GetKeyUp(KeyCode.X))
                {
                    //Ataque Ligero
                    if(attackHoldTime < heavyAttackHoldLength)
                    {
                        LightAttack();
                    }

                    attackHoldTime = 0;

                    canHeavyAttack = true;
                }
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

        //Contador de Ataque
        if(attackCounter > 0) 
        { 
            attackCounter -= Time.deltaTime;
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

    public void Bounce(float bounceForce)
    {
        _playerRB.velocity = new Vector2(_playerRB.velocity.x, bounceForce);
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

    void LightAttack()
    {
        if(_playerSpriteRenderer.flipX)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, whatIsEnemy);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(lightDamage);
            }
        }
        
        if (!_playerSpriteRenderer.flipX)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, whatIsEnemy);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(lightDamage);
            }
        }

        attackCounter = attackCounterLength;

        Debug.Log("Ataque Ligero");
    }

    void HeavyAttack()
    {
        if (_playerSpriteRenderer.flipX)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, whatIsEnemy);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(heavyDamage);
            }
        }

        if (!_playerSpriteRenderer.flipX)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, whatIsEnemy);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(heavyDamage);
            }
        }

        attackCounter = heavyAttackCounterLength;

        Debug.Log("Ataque Pesado");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointLeft != null)
        {
            Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        }

        if (attackPointRight != null)
        {
            Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        }
    }
}