using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool isEnemy, isProp, isBasicNyuxhian, isRangerNyuxhian, isHeavyNyuxhian;
    public float maxHealth;
    public float currentHealth;
    public bool isDamaged;

    public float spawnTimeLength;
    private float _spawnTime;

    SpriteRenderer _sPR;
    EnemySpawner _eSRef;
    Transform _playerTransform;

    private void Start()
    {
        currentHealth = maxHealth;

        _sPR = GetComponent<SpriteRenderer>();

        if(isBasicNyuxhian || isRangerNyuxhian || isHeavyNyuxhian)
        {
            _eSRef = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        }

        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if(_playerTransform.gameObject.activeSelf)
        {
            //Nyuxhiano Acorazado Spawnea
            if (_spawnTime > 0)
            {
                _spawnTime -= Time.deltaTime;
            }

            if (isHeavyNyuxhian && Vector2.Distance(transform.position, _playerTransform.position) < 17 && _spawnTime <= 0f)
            {
                _eSRef.SpawnEnemy();

                _spawnTime = spawnTimeLength;
            }
        }

        //Muerte
        if (currentHealth <= 0) 
        {
            if (isBasicNyuxhian)
            {
                _eSRef.SpawnEnemy();
                _eSRef.SpawnEnemy();
            }
            else if (isRangerNyuxhian)
            {
                _eSRef.SpawnEnemy();
                _eSRef.SpawnEnemy();
                _eSRef.SpawnEnemy();
            }
            else if (isHeavyNyuxhian)
            {
                _eSRef.SpawnEnemy();
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
            else if(!isHeavyNyuxhian && !isRangerNyuxhian)
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
