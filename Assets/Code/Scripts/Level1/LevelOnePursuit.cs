using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOnePursuit : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private CameraController _cCRef;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _cCRef = GameObject.Find("Main Camera").GetComponent<CameraController>();
        
        _cCRef.xOffSet = 1f;
        _cCRef.target = GameObject.Find("Pursuit(Clone)").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(speed, 0);
        transform.position = new Vector2(transform.position.x, _playerTransform.position.y);
    }
}
