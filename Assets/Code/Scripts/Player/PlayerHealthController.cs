using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public float invincibleCounterLength = 1f;
    public float invincibleCounter;

    private UIController _uIRef;

    private IkalController _iCRef;

    private SpriteRenderer _sRRef;

    private LevelManager _lMRef;

    public GameObject playerDeath;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;        

        _sRRef = GetComponent<SpriteRenderer>();
       
        _iCRef = GetComponent<IkalController>();

        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();

        _lMRef = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0)
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

    public void DealWithDamage()
    {
        if(invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0; //Por si se queda en negativo

                invincibleCounter = invincibleCounterLength;
            }
            else
            {
                invincibleCounter = invincibleCounterLength;

                _sRRef.color = new Color(255f, 127f, 127f, 0.7f);

                _iCRef.Knockback();

                _iCRef.jumpNumber = 0;
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
        _iCRef.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, _iCRef.gameObject.GetComponent<Rigidbody2D>().velocity.y);

        yield return new WaitUntil(() => _iCRef.isGrounded);

        Instantiate(playerDeath, transform.position, transform.rotation);

        _lMRef.RespawnPlayer();
    }
}
