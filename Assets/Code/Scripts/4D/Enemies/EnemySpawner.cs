using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, transform.rotation);

        enemy.transform.parent = transform;

        enemy.GetComponent<FourDimensionalEnemy>().SpawnRandom();
    }
}
