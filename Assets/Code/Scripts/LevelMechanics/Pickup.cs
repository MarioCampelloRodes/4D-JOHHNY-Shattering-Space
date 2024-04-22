using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isPoint, isIncreaseSpeed, isBigPoint;

    public bool isCollected;
    private UIController _uIRef;
    private PlayerHealthController _pHCRef;
    private IkalController _pCRef;
    // Start is called before the first frame update
    void Start()
    {
        _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
        _pHCRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
        _pCRef = GameObject.FindGameObjectWithTag("Player").GetComponent<IkalController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            if (isPoint)
            {
                _uIRef.AddScore(100);

                isCollected = true;

                AudioManager.aMRef.PlaySFX(13);

                Destroy(gameObject);
            }

            if(isBigPoint)
            {
                _uIRef.AddScore(1000);

                isCollected = true;

                AudioManager.aMRef.PlaySFX(13);

                Destroy(gameObject);
            }

            if (isIncreaseSpeed)
            {
                _pCRef.boostTime = _pCRef.boostTimeLength;

                isCollected = true;

                AudioManager.aMRef.PlaySFX(3);

                gameObject.SetActive(false);
            }
        }
    }
}
