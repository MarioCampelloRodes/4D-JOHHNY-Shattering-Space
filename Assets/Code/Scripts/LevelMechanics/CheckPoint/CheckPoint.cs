using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Sprite offCheckpoint, onCheckpoint;

    private SpriteRenderer _spriteRendererRef;

    private CheckpointController _cpControllerRef;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRendererRef = GetComponent<SpriteRenderer>();
        _cpControllerRef = GetComponentInParent<CheckpointController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _cpControllerRef.DeactivateCheckpoints();
            _spriteRendererRef.sprite = onCheckpoint;

            _cpControllerRef.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckPoint()
    {
        _spriteRendererRef.sprite = offCheckpoint;
    }
}
