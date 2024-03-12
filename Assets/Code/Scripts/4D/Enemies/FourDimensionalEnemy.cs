using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FourDimensionalEnemy : MonoBehaviour
{
    public float enemySpeed;

    private bool _isSpawned;

    private Rigidbody2D _rb;

    private Transform _spawnTop, _spawnBottom, _spawnLeft, _spawnRight;

    private PlayerHealthController _pHC;

    private EnemySpawner _enemySpawner;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.parent = GameObject.Find("EnemySpawner").transform;

        _pHC = GameObject.Find("Player").GetComponent<PlayerHealthController>();

        _spawnTop = GameObject.Find("TopSpawn").GetComponent<Transform>();
        _spawnBottom = GameObject.Find("BottomSpawn").GetComponent<Transform>();
        _spawnLeft = GameObject.Find("LeftSpawn").GetComponent<Transform>();
        _spawnRight = GameObject.Find("RightSpawn").GetComponent<Transform>();

        SpawnRandom();
    }
    private void Update()
    {
        if (_pHC.currentHealth <= 0f) 
        {
            Destroy(gameObject, 0.8f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("4DPlayer"))
        {
            _pHC.DealWithDamage();

            _isSpawned = false;

            SpawnRandom();
        }
    }

    public void SpawnRandom()
    {
        int spawnRandomPos = Random.Range(1, 5);

        switch (spawnRandomPos)
        {
            case 1:
                if (!_isSpawned)
                {
                    transform.position = _spawnTop.position;
                    _rb.velocity = new Vector2(0f, -enemySpeed);
                    _isSpawned = true;
                }
                break;

            case 2:
                if (!_isSpawned)
                {
                    transform.position = _spawnBottom.position;
                    _rb.velocity = new Vector2(0f, enemySpeed);
                    _isSpawned = true;
                }
                break;

            case 3:
                if (!_isSpawned)
                {
                    transform.position = _spawnLeft.position;
                    _rb.velocity = new Vector2(enemySpeed, 0f);
                    _isSpawned = true;
                }
                break;

            case 4:
                if (!_isSpawned)
                {
                    transform.position = _spawnRight.position;
                    _rb.velocity = new Vector2(-enemySpeed, 0f);
                    _isSpawned = true;
                }
                break;

            default:
                if (!_isSpawned)
                {
                    transform.position = _spawnLeft.position;
                    _rb.velocity = new Vector2(enemySpeed, 0f);
                    _isSpawned = true;
                }
                
                break;
        }
    }
}
