using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab, weakSpotPrefab;

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, this.transform.position + new Vector3(0, 3.25f, 0), this.transform.rotation);

        enemy.transform.parent = this.transform;

        GetComponentInChildren<FourDimensionalEnemy>().SpawnRandom();
    }

    public void SpawnWeakSpot()
    {
        GameObject weakSpot = Instantiate(weakSpotPrefab, this.transform.position, this.transform.rotation);

        weakSpot.transform.parent = this.transform;

        GetComponentInChildren<FourDimensionalWeakSpot>().SpawnRandom();
    }
}
