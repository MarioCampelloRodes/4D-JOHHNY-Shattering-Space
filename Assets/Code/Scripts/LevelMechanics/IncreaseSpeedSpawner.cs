using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedSpawner : MonoBehaviour
{
    private bool _canActivate = true;
    public GameObject increaseSpeedPrefab;
    public GameObject increaseSpeedChidren;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(!increaseSpeedChidren.activeSelf && _canActivate)
        {
            SpawnIncreaseSpeed();
        }
    }

    private void SpawnIncreaseSpeed()
    {
        StartCoroutine(SpawnIncreaseSpeedCO());
    }

    private IEnumerator SpawnIncreaseSpeedCO()
    {
        increaseSpeedChidren.GetComponent<Pickup>().isCollected = false;

        _canActivate = false;

        yield return new WaitForSeconds(4f);

        increaseSpeedChidren.SetActive(true);

        _canActivate = true;
    }
}
