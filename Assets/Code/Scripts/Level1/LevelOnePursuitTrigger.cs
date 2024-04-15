using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOnePursuitTrigger : MonoBehaviour
{
    public GameObject pursuitPrefab;

    private bool _isActive;
    private LevelOneLM _lMRef;

    private void Start()
    {
        _lMRef = GameObject.Find("L1LevelManager").GetComponent<LevelOneLM>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isActive)
        {
            Instantiate(pursuitPrefab, transform.position, transform.rotation);
            _lMRef.isPursuitActive = true;
            _isActive = true;
        }
    }
}
