using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private UIController _uIRef;
    // Start is called before the first frame update
    void Start()
    {
        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _uIRef.AddScore(100);

            Destroy(gameObject);
        }
    }
}
