using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed;
    public Transform maxLeft, maxRight;
    private bool _movingRight;
    
    private Rigidbody2D _rb;
    private GameObject _ikal;
    private IkalController _iCRef;
    // Start is called before the first frame update
    void Start()
    {
        maxLeft.transform.parent = null; 
        maxRight.transform.parent = null;

        _movingRight = true;

        _rb = GetComponent<Rigidbody2D>();
        _ikal = GameObject.Find("Ikal");
        _iCRef = GameObject.Find("Ikal").GetComponent<IkalController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_movingRight) 
        {
            _rb.velocity = new Vector2(platformSpeed, 0f);            
        }
        else
        {
            _rb.velocity = new Vector2(-platformSpeed, 0f);
        }

        if(transform.position.x >= maxRight.position.x) 
        {
            _movingRight = false;
        }
        else if (transform.position.x <= maxLeft.position.x)
        {
            _movingRight = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _iCRef.isGrounded)
        {
            if(_ikal.gameObject.activeSelf)
                _ikal.transform.position = new Vector2(_ikal.transform.position.x + _rb.velocity.x * Time.deltaTime, _ikal.transform.position.y);
            
        }
    }
}
