using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [HideInInspector] public int currentHealth;
    public int maxHealth;

    public float invincibleCounterLength = 1;
    public float invincibleCounter;

    private UIController _uIRef;

    private PlayerController _pCRef;

    private SpriteRenderer _spriteRendererRef;

    private LevelManager _lMRef;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();

        _pCRef = GetComponent<PlayerController>();

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
    }

    public void DealWithDamage()
    {
        if(invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0; //Por si se queda en negativo
                _lMRef.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleCounterLength;

                _spriteRendererRef.color = new Color(255f, 127f, 127f, 0.7f);

                _pCRef.Knockback();

                _pCRef.jumpNumber = 0;
            }
            _uIRef.UpdateHealth();
        }
    }
}
