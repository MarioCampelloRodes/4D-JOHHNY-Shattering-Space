using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FourDBullets : MonoBehaviour
{
    public float xSpeed, ySpeed;

    private bool _isShot;

    private Rigidbody2D _rb;

    private Transform _spawnTop, _spawnBottom, _spawnLeft, _spawnRight;

    private FourDPlayer _fDPRef;

    private UIController _uIRef;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.parent = GameObject.Find("JohnnyIcon").transform;

        _spawnTop = GameObject.Find("TopSpawn").GetComponent<Transform>();
        _spawnBottom = GameObject.Find("BottomSpawn").GetComponent<Transform>();
        _spawnLeft = GameObject.Find("LeftSpawn").GetComponent<Transform>();
        _spawnRight = GameObject.Find("RightSpawn").GetComponent<Transform>();

        _fDPRef = GetComponentInParent<FourDPlayer>();

        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_fDPRef.shootingLeft && !_isShot)
        {
            _rb.velocity = new Vector2(-xSpeed, 0f);
            _isShot = true;
        }
        if (_fDPRef.shootingRight && !_isShot)
        {
            _rb.velocity = new Vector2(xSpeed, 0f);
            _isShot = true;
        }
        if (_fDPRef.shootingTop && !_isShot)
        {
            _rb.velocity = new Vector2(0f, xSpeed);
            _isShot = true;
        }
        if (_fDPRef.shootingBottom && !_isShot)
        {
            _rb.velocity = new Vector2(0f, -xSpeed);
            _isShot = true;
        }

        if (transform.position.x >= _spawnRight.position.x || transform.position.y >= _spawnTop.position.y || transform.position.x <= _spawnLeft.position.x || transform.position.y <= _spawnBottom.position.y) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("4DEnemy") || collision.CompareTag("4DWeakSpot"))
        {
            _uIRef.AddScore(200);

            AudioManager.aMRef.PlaySFX(8);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
