using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed;
    public Transform maxLeft, maxRight;
    private bool _movingRight;

    private Rigidbody2D _rb;
    private IkalController _iCRef;
    // Start is called before the first frame update
    void Start()
    {
        maxLeft.transform.parent = null;
        maxRight.transform.parent = null;

        _movingRight = true;

        _rb = GetComponent<Rigidbody2D>();
        _iCRef = GameObject.Find("Ikal").GetComponent<IkalController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, maxRight.position, platformSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, maxLeft.position, platformSpeed * Time.deltaTime);
        }

        if (transform.position.x >= maxRight.position.x)
        {
            _movingRight = false;
        }
        else if (transform.position.x <= maxLeft.position.x)
        {
            _movingRight = true;
        }
    }
}