using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public bool isIkal, isJohnny;
    public int currentHealth;
    public int maxHealth;

    public float invincibleCounterLength = 1f;
    public float invincibleCounter;

    private UIController _uIRef;

    private IkalController _iCRef;

    private JohnnyController _jCRef;

    private SpriteRenderer _spriteRendererRef;

    private LevelManager _lMRef;

    public GameObject playerDeath;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();

        if(isIkal)
        {
            IkalController _iCRef = GetComponent<IkalController>();
        }

        if (isJohnny)
        {
            _jCRef = GetComponent<JohnnyController>();
        }

        _spriteRendererRef = GetComponent<SpriteRenderer>();

        _lMRef = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            _spriteRendererRef.color = new Color(_spriteRendererRef.color.r, _spriteRendererRef.color.g, _spriteRendererRef.color.b, 0.7f);
        }
        else
        {
            _spriteRendererRef.color = Color.white;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0; //Por si se queda en negativo

            invincibleCounter = invincibleCounterLength;

            Death();
        }
    }

    public void DealWithDamage()
    {
        if(invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0; //Por si se queda en negativo

                invincibleCounter = invincibleCounterLength;

                Death();
            }
            else
            {
                invincibleCounter = invincibleCounterLength;

                _spriteRendererRef.color = new Color(255f, 127f, 127f, 0.7f);

                if (isIkal)
                {
                    _iCRef.Knockback();

                    _iCRef.jumpNumber = 0;
                }

                if (isJohnny)
                {
                    _jCRef.Knockback();
                }

            }
            _uIRef.UpdateHealth();
        }
    }

    public void Death()
    {
        StartCoroutine("DeathCO");
    }

    private IEnumerator DeathCO()
    {
        if (isIkal)
        {
            _iCRef.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, _iCRef.gameObject.GetComponent<Rigidbody2D>().velocity.y);

            yield return new WaitUntil(() => _iCRef.isGrounded);

            _lMRef.RespawnPlayer();

            Instantiate(playerDeath, transform.position, transform.rotation);

            yield return new WaitForSeconds(1f);
        }
        if (isJohnny)
        {
            _jCRef.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, _jCRef.gameObject.GetComponent<Rigidbody2D>().velocity.y);

            yield return new WaitUntil(() => _jCRef.isGrounded);

            _lMRef.RespawnPlayer();

            Instantiate(playerDeath, transform.position, transform.rotation);

            yield return new WaitForSeconds(1f);
        }
        
    }
}
