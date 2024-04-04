using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FourDimensionalWeakSpot : MonoBehaviour
{
    private bool _isSpawned;

    private Transform _spawnTop, _spawnBottom, _spawnLeft, _spawnRight;

    private EnemySpawner _enemySpawner;
    // Start is called before the first frame update
    void Awake()
    {
        transform.parent = GameObject.Find("EnemySpawner").transform;

        _spawnTop = GameObject.Find("TopSpawn").GetComponent<Transform>();
        _spawnBottom = GameObject.Find("BottomSpawn").GetComponent<Transform>();
        _spawnLeft = GameObject.Find("LeftSpawn").GetComponent<Transform>();
        _spawnRight = GameObject.Find("RightSpawn").GetComponent<Transform>();

        SpawnRandom();
    }

    public void SpawnRandom()
    {
        int spawnRandomPos = Random.Range(1, 5);

        switch (spawnRandomPos)
        {
            case 1:
                if (!_isSpawned)
                {
                    transform.position = _spawnTop.position + new Vector3(0, -0.6f, 0);
                    _isSpawned = true;
                }
            break;

            case 2:
                if (!_isSpawned)
                {
                    transform.position = _spawnBottom.position + new Vector3(0, 0.6f, 0);
                    _isSpawned = true;
                }
            break;

            case 3:
                if (!_isSpawned)
                {
                    transform.position = _spawnLeft.position + new Vector3(0.6f, 0, 0);
                    _isSpawned = true;
                }
            break;

            case 4:
                if (!_isSpawned)
                {
                    transform.position = _spawnRight.position + new Vector3(-0.6f, 0, 0);
                    _isSpawned = true;
                }
            break;

            default:
                if (!_isSpawned)
                {
                    transform.position = _spawnLeft.position + new Vector3(0.6f, 0, 0);
                    _isSpawned = true;
                }

            break;
        }
    }
}
