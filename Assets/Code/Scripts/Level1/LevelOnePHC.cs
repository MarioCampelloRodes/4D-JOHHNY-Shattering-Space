using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOnePHC : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public float invincibleCounterLength = 1f;
    public float invincibleCounter;

    private UIController _uIRef;

    private JohnnyController _jCRef;

    private SpriteRenderer _sRRef;

    private LevelOneLM _lMRef;

    public GameObject playerDeath;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        _sRRef = GetComponent<SpriteRenderer>();

        _jCRef = GetComponent<JohnnyController>();

        _lMRef = GameObject.Find("L1LevelManager").GetComponent<LevelOneLM>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            _sRRef.color = new Color(_sRRef.color.r, _sRRef.color.g, _sRRef.color.b, 0.7f);
        }
        else
        {
            _sRRef.color = Color.white;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0; //Por si se queda en negativo

            invincibleCounter = invincibleCounterLength;

            Death();
        }
    }

    public void DealWithDamage(int damage)
    {
        if (invincibleCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0; //Por si se queda en negativo

                invincibleCounter = invincibleCounterLength;

                Death();
            }
            else
            {
                invincibleCounter = invincibleCounterLength;

                _sRRef.color = new Color(255f, 127f, 127f, 0.7f);

                _jCRef.Knockback();

            }
        }
    }

    public void Death()
    {
        StartCoroutine("DeathCO");
    }

    private IEnumerator DeathCO()
    {
        _jCRef.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, _jCRef.gameObject.GetComponent<Rigidbody2D>().velocity.y);

        yield return new WaitUntil(() => _jCRef.isGrounded);

        _lMRef.RespawnPlayer();

        Instantiate(playerDeath, transform.position, transform.rotation);

        yield return new WaitForSeconds(1f);
    }
}
