using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDSpawnTrigger : MonoBehaviour
{
    private bool hasTriggered;
    private EnemySpawner _eSRef;

    // Start is called before the first frame update
    private void Start()
    {
        _eSRef = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasTriggered)
        {
            StartCoroutine(CallSpawnEnemyCO());

            hasTriggered = true;
        }
    }

    private IEnumerator CallSpawnEnemyCO()
    {
        _eSRef.SpawnEnemy();

        yield return new WaitForSeconds(0.5f);

        _eSRef.SpawnEnemy();
    }
}
