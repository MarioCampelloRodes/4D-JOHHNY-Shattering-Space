using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D _platformCollider;
    private bool _onPlatform;
    // Start is called before the first frame update
    void Start()
    {
        _platformCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (_onPlatform && Input.GetAxis("Vertical") < -0.1f)
        {
            StartCoroutine(ActDeactPlatform());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _onPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _onPlatform = false;
    }

    private IEnumerator ActDeactPlatform()
    {
        _platformCollider.enabled = false;

        yield return new WaitForSeconds(0.5f);

        _platformCollider.enabled = true;
    }
}