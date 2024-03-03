using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float slimeSpeed;
    public Transform maxLeft, maxRight;
    public bool movingRight;

    public float moveTime, waitTime;
    private float _moveCount, _waitCount;

    public bool seeingLeft, seeingRight;

    private Rigidbody2D _rBRef;
    private SpriteRenderer _sRRef;
    private Animator _animRef;
    // Start is called before the first frame update
    void Start()
    {
        _rBRef = GetComponent<Rigidbody2D>();
        _sRRef = GetComponentInChildren<SpriteRenderer>();
        _animRef = GetComponent<Animator>();

        maxLeft.parent = null;
        maxRight.parent = null;

        movingRight = true;

        _moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(_moveCount > 0)
        {
            _moveCount -= Time.deltaTime;

            if (movingRight)
            {
                if (seeingRight)
                {
                    _rBRef.velocity = new Vector2(slimeSpeed * 1.75f, _rBRef.velocity.y);
                    _moveCount = moveTime;
                }
                else if (!seeingRight)
                {
                    _rBRef.velocity = new Vector2(slimeSpeed, _rBRef.velocity.y);
                }
                
                _sRRef.flipX = true;

                if (transform.position.x >= maxRight.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                if (seeingLeft)
                {
                    _rBRef.velocity = new Vector2(-slimeSpeed * 1.75f, _rBRef.velocity.y);
                    _moveCount = moveTime;
                }
                else if (!seeingLeft)
                {
                    _rBRef.velocity = new Vector2(-slimeSpeed, _rBRef.velocity.y);
                }

                _sRRef.flipX = false;

                if (transform.position.x <= maxLeft.position.x)
                {
                    movingRight = true;
                }
            }

            if(_moveCount <= 0)
            {
                _waitCount = waitTime;
            }

            //_animRef.SetBool("IsMoving", true);
        }
        else if(_waitCount > 0)
        {
            _waitCount -= Time.deltaTime;

            _rBRef.velocity = new Vector2(0f, _rBRef.velocity.y);

            if(_waitCount <= 0)
            {
                _moveCount = Random.Range(moveTime * 0.5f, moveTime * 1.25f);
            }

            //_animRef.SetBool("IsMoving", false);
        } 
    }
}
