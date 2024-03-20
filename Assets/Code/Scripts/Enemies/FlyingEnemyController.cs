using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public float speed;

    public Transform[] pointsToMove;

    public int currentPoint;

    public float distanceToAttackPlayer, chaseSpeed;

    public float attackTimeLength;
    private float _attackTime;

    private Vector3 _attackTarget;

    private GameObject _player;

    private SpriteRenderer _sRRef;
    // Start is called before the first frame update
    void Start()
    {
        _sRRef = GetComponentInChildren<SpriteRenderer>();

        _player = GameObject.Find("Player");

        foreach(Transform point in pointsToMove)
        {
            point.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_attackTime > 0)
        {
            _attackTime -= Time.deltaTime;
        }
        else
        {
            if(Vector3.Distance(transform.position, _player.transform.position) > distanceToAttackPlayer)
            {
                _attackTarget = Vector3.zero;

                transform.position = Vector3.MoveTowards(transform.position, pointsToMove[currentPoint].position, speed * Time.deltaTime);

                if(Vector3.Distance(transform.position, pointsToMove[currentPoint].position) < 0.01f)
                {
                    currentPoint++;

                    if(currentPoint >= pointsToMove.Length)
                    {
                        currentPoint = 0;
                    }
                }

                if(transform.position.x < pointsToMove[currentPoint].position.x)
                {
                    _sRRef.flipX = true;
                }
                else if (transform.position.x > pointsToMove[currentPoint].position.x)
                {
                    _sRRef.flipX = false;
                }
            }
            else
            {
                if(_attackTarget == Vector3.zero)
                {
                    _attackTarget = _player.transform.position;
                }

                transform.position = Vector3.MoveTowards(transform.position, _attackTarget, chaseSpeed * Time.deltaTime);

                if (transform.position.x < _attackTarget.x)
                {
                    _sRRef.flipX = true;
                }
                else if (transform.position.x > _attackTarget.x)
                {
                    _sRRef.flipX = false;
                }

                if (Vector3.Distance(transform.position, _attackTarget) <= 0.1f) //Por si está muy cerca del player que pueda seguir su camino
                {
                    _attackTime = attackTimeLength;
                    _attackTarget = Vector3.zero;
                }
            }
        }

        
    }
}
