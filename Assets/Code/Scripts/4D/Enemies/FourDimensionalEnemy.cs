using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDimensionalEnemy : MonoBehaviour
{
    public float enemySpeed;

    private Rigidbody2D _rb;

    private Transform _spawnTop, _spawnBottom, _spawnLeft, _spawnRight;

    private PlayerHealthController _pHC;

    private EnemySpawner _enemySpawner;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _pHC = GameObject.Find("Player").GetComponent<PlayerHealthController>();

        _spawnTop = GameObject.Find("TopSpawn").GetComponent<Transform>();
        _spawnBottom = GameObject.Find("BottomSpawn").GetComponent<Transform>();
        _spawnLeft = GameObject.Find("LeftSpawn").GetComponent<Transform>();
        _spawnRight = GameObject.Find("RightSpawn").GetComponent<Transform>();

        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        SpawnRandom();

    }
    private void Update()
    {
        transform.parent = _enemySpawner.gameObject.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("4DPlayer"))
        {
            _pHC.DealWithDamage();

            SpawnRandom();
        }
    }

    public void SpawnRandom()
    {
        int spawnRandomPos = Random.Range(1, 5);

        switch (spawnRandomPos)
        {
            case 1:
                transform.position = _spawnTop.position;
                _rb.velocity = new Vector2(0f, -enemySpeed);
                break;

            case 2:
                transform.position = _spawnBottom.position;
                _rb.velocity = new Vector2(0f, enemySpeed);
                break;

            case 3:
                transform.position = _spawnLeft.position;
                _rb.velocity = new Vector2(enemySpeed, 0f);
                break;
            case 4:
                transform.position = _spawnRight.position;
                _rb.velocity = new Vector2(-enemySpeed, 0f);
                break;
            default:
                transform.position = _spawnLeft.position;
                _rb.velocity = new Vector2(enemySpeed, 0f);
                break;
        }
    }
}
