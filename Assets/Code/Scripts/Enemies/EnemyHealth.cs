using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public bool isEnemy, isProp, isBasicNyuxhian, isRangerNyuxhian, isHeavyNyuxhian, isBoss;
    public float maxHealth;
    public float currentHealth;
    public bool isDamaged;
    public int enemiesSpawned;
    public int scoreReward;

    //Nyuxhiano Acorazado
    public float spawnTimeLength;
    private float _spawnTime;

    //Nyuxhiano Tirador
    private bool _hasAppeared, _isInvincible;
    public float shootTimeLength;
    private float _shootTime;
    public GameObject leftBulletPrefab, rightBulletPrefab;
    private GameObject _weakSpot;

    //Boss
    public int howManyBossHealthQuarters = 4;

    BossController _bossController;
    SpriteRenderer _sPR;
    EnemySpawner _eSRef;
    private float _lookForPlayerTime;
    GameObject _player;
    UIController _uIRef;
    public Image bossLifeBar;

    private Animator _anim;

    private void Start()
    {
        currentHealth = maxHealth;

        _sPR = GetComponent<SpriteRenderer>();

        if(isBasicNyuxhian || isRangerNyuxhian || isHeavyNyuxhian)
        {
            _eSRef = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        }

        if(isRangerNyuxhian)
        {
            _isInvincible = true;
            _anim = GetComponent<Animator>();
        }

        _player = GameObject.FindWithTag("Player");
        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
        _bossController = GetComponent<BossController>();
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
                Instantiate(leftBulletPrefab, transform.position + new Vector3(-1.5f, -0.5f, 0), transform.rotation);
                _sPR.flipX = false;
            }
            else if (_player.transform.position.x > transform.position.x)
            {
                Instantiate(rightBulletPrefab, transform.position + new Vector3(1.5f, -0.5f, 0), transform.rotation);
                _sPR.flipX = true;
            }

            AudioManager.aMRef.PlaySFX(9);

            _shootTime = shootTimeLength;
        }
        if (isRangerNyuxhian && _shootTime > 0)
        {
            _shootTime -= Time.deltaTime;
        }

        //Nyuxhiano Tirador Animaci�n
        if (isRangerNyuxhian && Vector2.Distance(transform.position, _player.transform.position) <= 17)
        {
            _anim.SetBool("Shoot", true);
        }
        else if (isRangerNyuxhian && Vector2.Distance(transform.position, _player.transform.position) > 17)
        {
            _anim.SetBool("Shoot", false);
        }

        //Nyuxhiano Tirador Punto D�bil
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

            _uIRef.AddScore(scoreReward);
            _uIRef.AddStreak();

            EnemyDeathController();
        }
    }
    public GameObject deathEffect;
    // Start is called before the first frame update
    
    void EnemyDeathController()
    {
        AudioManager.aMRef.PlaySFX(8);
        Instantiate(deathEffect, transform.position, transform.rotation);
        if (isBoss)
        {
            AudioManager.aMRef.bossMusic.Stop();

            GameObject.Find("BossLifeBar").SetActive(false);
        }
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
            else if (isHeavyNyuxhian && damage < maxHealth)
            {
                AudioManager.aMRef.PlaySFX(2);
            }
            else if(!isHeavyNyuxhian && !_isInvincible)
            {
                currentHealth -= damage;

                _sPR.color = new Color(_sPR.color.r, _sPR.color.g, _sPR.color.b, 0.7f);

                isDamaged = true;

                if (isBoss)
                {
                    UpdateBossBar();

                    if(currentHealth < maxHealth * 0.25)
                    {
                        howManyBossHealthQuarters = 1;
                    }
                    else if(currentHealth < maxHealth * 0.5)
                    {
                        howManyBossHealthQuarters = 2;

                        _bossController.shootCooldown = 1.25f;
                        _bossController.pursuitCooldown = 1.25f;
                        _bossController.spawnCooldown = 0.75f;
                    }
                    else if(currentHealth < maxHealth * 0.75)
                    {
                        howManyBossHealthQuarters = 3;
                    }
                    else if(currentHealth < maxHealth)
                    {
                        howManyBossHealthQuarters = 4;
                    }
                    
                    //Debug.Log("Al boss le quedan " + howManyBossHealthQuarters + " cuartos de vida.");
                }

                yield return new WaitForSeconds(0.5f);

                _sPR.color = new Color(_sPR.color.r, _sPR.color.g, _sPR.color.b, 1f);

                isDamaged = false;
            }

        }

        if (isProp && damage >= maxHealth) 
        {
            currentHealth -= damage;
        }
        else if (isProp && damage < maxHealth)
        {
            AudioManager.aMRef.PlaySFX(2);
        }
    }

    public void UpdateBossBar()
    {
        bossLifeBar.rectTransform.sizeDelta = new Vector2((currentHealth * 586) / maxHealth, bossLifeBar.rectTransform.sizeDelta.y);
    }
}
