using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IkalController : MonoBehaviour
{
    //¿Tiene el control?
    public bool canMove;
    public bool isLevelOver;
    //Movimiento Default
    public float playerSpeed;
    
    //Saltos
    public float playerJumpForce;
    public int jumpNumber = 0;
    public bool isGrounded;
    public float fallSpeed;

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
    private bool _isHeavyAttacking;
    public GameObject leftBulletPrefab, rightBulletPrefab, attackCirclePrefab;

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
        //¿Está en el suelo?
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        //¿Tiene el control?
        if (_knockbackCounter <= 0 && _wallJumpCounter <= 0 && !_isDashing && _pHCRef.currentHealth >= 0 && !isLevelOver && !_isHeavyAttacking)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
        //Si los contadores se han vaciado, el jugador recupera el control
        if (canMove)
        {
            //Movimiento
            if (boostTime > 0) //Movimiento Boost
            {
                if (isGrounded && !_isDashing)
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * boostSpeed, _playerRB.velocity.y);
                else if ((Input.GetAxisRaw("Horizontal") > 0.1f && _playerRB.velocity.x <= -dashSpeed * 0.95) || (Input.GetAxisRaw("Horizontal") < -0.1f && _playerRB.velocity.x >= dashSpeed * 0.95))
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * boostSpeed, _playerRB.velocity.y);
                else if (_playerRB.velocity.x > -dashSpeed * 0.95 && _playerRB.velocity.x < dashSpeed * 0.95)
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * boostSpeed, _playerRB.velocity.y);

                if (!isGrounded && Input.GetAxisRaw("Vertical") <= -0.1f)
                    _playerRB.velocity = new Vector2(0f, _playerRB.velocity.y - 0.1f);
            }
            else //Movimiento Default
            {
                if (isGrounded && !_isDashing)
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, _playerRB.velocity.y);
                else if ((Input.GetAxisRaw("Horizontal") > 0.1f && _playerRB.velocity.x <= -dashSpeed * 0.95) || (Input.GetAxisRaw("Horizontal") < -0.1f && _playerRB.velocity.x >= dashSpeed * 0.95))
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, _playerRB.velocity.y);
                else if (_playerRB.velocity.x > -dashSpeed * 0.95 && _playerRB.velocity.x < dashSpeed * 0.95)
                    _playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerSpeed, _playerRB.velocity.y);

                if (!isGrounded && Input.GetAxisRaw("Vertical") <= -0.1f)
                    _playerRB.velocity = new Vector2(0f, _playerRB.velocity.y - 0.1f);
            }

            //¿Está tocando la pared?
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
                if (isGrounded && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightShift)))
                {
                    HeavyAttack();
                }
                if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.RightControl))
                {
                    LightAttack();
                }
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

        //Límite Velocidad de Caída
        if (_playerRB.velocity.y < fallSpeed)
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, fallSpeed);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = null;
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

            if (hitEnemies.Length == 0 && SceneManager.GetActiveScene().name != "Level-2")
            {
                Instantiate(rightBulletPrefab, transform.position + new Vector3(3f, 0, 0), transform.rotation);
            }
            
            Instantiate(attackCirclePrefab, attackPointRight.position, transform.rotation);
        }
        
        if (!_playerSpriteRenderer.flipX)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, whatIsEnemy);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(lightDamage);
            }

            if(hitEnemies.Length == 0 && SceneManager.GetActiveScene().name != "Level-2")
            {
                Instantiate(leftBulletPrefab, transform.position + new Vector3(-3f, 0, 0), transform.rotation);
            }

            Instantiate(attackCirclePrefab, attackPointLeft.position, transform.rotation);
        }

        attackCounter = attackCounterLength;

        Debug.Log("Ataque Ligero");
    }

    void HeavyAttack()
    {
        StartCoroutine(HeavyAttackCO());
    }

    IEnumerator HeavyAttackCO()
    {
        _isHeavyAttacking = true;

        _playerRB.velocity = new Vector2(0f, _playerRB.velocity.y);

        yield return new WaitForSeconds(0.3f);

        if (_playerSpriteRenderer.flipX)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, whatIsEnemy);


            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(heavyDamage);
            }

            Instantiate(attackCirclePrefab, attackPointRight.position, transform.rotation);
        }

        if (!_playerSpriteRenderer.flipX)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, whatIsEnemy);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(heavyDamage);
            }

            Instantiate(attackCirclePrefab, attackPointLeft.position, transform.rotation);
        }

        attackCounter = heavyAttackCounterLength;

        Debug.Log("Ataque Pesado");

        yield return new WaitForSeconds(0.3f);

        _isHeavyAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (attackPointLeft != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        }

        if (attackPointRight != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        }
    }
}