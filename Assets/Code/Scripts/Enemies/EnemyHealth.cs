using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyHealth : MonoBehaviour
{
    public bool isEnemy, isProp, isBasicNyuxhian, isRangerNyuxhian, isHeavyNyuxhian;
    public float maxHealth;
    public float currentHealth;
    public bool isDamaged;
    public int enemiesSpawned;

    //Nyuxhiano Acorazado
    public float spawnTimeLength;
    private float _spawnTime;

    //Nyuxhiano Tirador
    private bool _hasAppeared, _isInvincible;
    public float shootTimeLength;
    private float _shootTime;
    public GameObject leftBulletPrefab, rightBulletPrefab;
    private GameObject _weakSpot;

    SpriteRenderer _sPR;
    EnemySpawner _eSRef;
    private float _lookForPlayerTime;
    GameObject _player;

    private void Start()
    {
        currentHealth = maxHealth;

        _sPR = GetComponent<SpriteRenderer>();

        if(isBasicNyuxhian || isRangerNyuxhian || isHeavyNyuxhian)
        {
            _eSRef = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        }

        if(isRangerNyuxhian )
        {
            _isInvincible = true;
        }

        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        //Nyuxhiano Acorazado Spawnea
        if (isHeavyNyuxhian && Vector2.Distance(transform.position, _player.transform.position) < 17 && _spawnTime <= 0f && _player.gameObject.activeSelf == true)
        {
            _eSRef.SpawnEnemy();

            _spawnTime = spawnTimeLength;
        }
        else if (isHeavyNyuxhian && _spawnTime > 0)
        {
            _spawnTime -= Time.deltaTime;
        }

        //Nyuxhiano Tirador Disparo
        if (isRangerNyuxhian && Vector2.Distance(transform.position, _player.transform.position) < 17 && _shootTime <= 0)
        {
            if (_player.transform.position.x < transform.position.x)
            {
                Instantiate(leftBulletPrefab, transform.position + new Vector3(-1f, 0, 0), transform.rotation);
            }
            else if (_player.transform.position.x > transform.position.x)
            {
                Instantiate(rightBulletPrefab, transform.position + new Vector3(1f, 0, 0), transform.rotation);
            }

            _shootTime = shootTimeLength;
        }
        if (isRangerNyuxhian && _shootTime > 0)
        {
            _shootTime -= Time.deltaTime;
        }

        //Nyuxhiano Tirador Punto Débil
        if (isRangerNyuxhian && Vector2.Distance(transform.position, _player.transform.position) < 17 && !_hasAppeared)
        {
            _eSRef.SpawnWeakSpot();

            _weakSpot = GameObject.FindWithTag("4DWeakSpot");

            _hasAppeared = true;
        }

        if (isRangerNyuxhian && _hasAppeared && _weakSpot == null && _isInvincible)
        {
            _isInvincible = false;

            _sPR.color = Color.white;
        }

        //Muerte
        if (currentHealth <= 0) 
        {
            if(enemiesSpawned > 0)
            {
                for(int i = 0; i < enemiesSpawned; i++) 
                {
                    _eSRef.SpawnEnemy();            
                }
            }

            EnemyDeathController();
        }
    }
    public GameObject deathEffect;
    // Start is called before the first frame update
    
    void EnemyDeathController()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(TakeDamageCO(damage));
    }

    private IEnumerator TakeDamageCO(int damage)
    {
        if(isEnemy)
        {
            if (isHeavyNyuxhian && damage >= maxHealth)
            {
                isHeavyNyuxhian = false;

                _sPR.color = new Color(1f, 0.5f, 0.5f, 1f);
            }
            else if(!isHeavyNyuxhian && !_isInvincible)
            {
                currentHealth -= damage;

                _sPR.color = new Color(_sPR.color.r, _sPR.color.g, _sPR.color.b, 0.7f);

                isDamaged = true;

                yield return new WaitForSeconds(0.5f);

                _sPR.color = new Color(_sPR.color.r, _sPR.color.g, _sPR.color.b, 1f);

                isDamaged = false;
            }

        }

        if (isProp && damage >= maxHealth) 
        {
            currentHealth -= damage;
        }
    }
}
