using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab, weakSpotPrefab;
    private GameObject _enemy, _weakSpot;

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);

        _enemy = GameObject.FindWithTag("4DEnemy");

        _enemy.transform.parent = this.transform;

        GetComponentInChildren<FourDimensionalEnemy>().SpawnRandom();
    }

    public void SpawnWeakSpot()
    {
        Instantiate(weakSpotPrefab, this.transform.position, this.transform.rotation);

        _weakSpot = GameObject.FindWithTag("4DWeakSpot");

        _weakSpot.transform.parent = this.transform;

        GetComponentInChildren<FourDimensionalWeakSpot>().SpawnRandom();
    }
}
