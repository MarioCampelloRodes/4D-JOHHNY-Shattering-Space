using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPursuit : MonoBehaviour
{
    private SpriteRenderer _sPRef;
    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _sPRef = GetComponent<SpriteRenderer>();
        
        _collider = GetComponent<Collider2D>();

        StartCoroutine(EnableColliderCO());
    }

    IEnumerator EnableColliderCO()
    {
        yield return new WaitForSeconds(0.75f);

        _sPRef.color = Color.white;

        _collider.enabled = true;

        yield return new WaitForSeconds(0.25f);

        Destroy(gameObject);
    }
}
