using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private Animator _anim;

    public float bounceForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IkalController>().Bounce(bounceForce);

            AudioManager.aMRef.PlaySFX(4);

            _anim.SetTrigger("Bounce");
        }
    }
}
