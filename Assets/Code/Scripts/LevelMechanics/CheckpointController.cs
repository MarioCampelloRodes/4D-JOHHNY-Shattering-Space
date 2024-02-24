using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private CheckPoint[] _checkpoints;

    private GameObject _playerRef;

    public Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        _checkpoints = GetComponentsInChildren<CheckPoint>();
        _playerRef = GameObject.Find("Player");
        spawnPoint = _playerRef.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints()
    {
        foreach(CheckPoint i in _checkpoints)
        {
            i.ResetCheckPoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
