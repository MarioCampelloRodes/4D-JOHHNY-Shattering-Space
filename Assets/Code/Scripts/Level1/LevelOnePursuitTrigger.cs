using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOnePursuitTrigger : MonoBehaviour
{
    public GameObject pursuitPrefab;

    private LevelOneLM _lMRef;

    private void Start()
    {
        _lMRef = GameObject.Find("L1LevelManager").GetComponent<LevelOneLM>();
    }

    public void SpawnPursuit()
    {
        Instantiate(pursuitPrefab, transform.position, transform.rotation);
        _lMRef.isPursuitActive = true;
    }
}
