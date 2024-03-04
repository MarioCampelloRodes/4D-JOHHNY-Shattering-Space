using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject _enemy;

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, this.transform.position, this.transform.rotation);

        _enemy = GameObject.FindWithTag("4DEnemy");

        _enemy.transform.parent = this.transform;

        GetComponentInChildren<FourDimensionalEnemy>().SpawnRandom();
    }
}
