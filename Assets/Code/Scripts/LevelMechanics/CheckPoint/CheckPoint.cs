using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private bool _isActive;
    public Sprite offCheckpoint, onCheckpoint;

    private SpriteRenderer _spriteRendererRef;

    private CheckpointController _cpControllerRef;

    private UIController _uIRef;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRendererRef = GetComponent<SpriteRenderer>();
        _cpControllerRef = GetComponentInParent<CheckpointController>();

        if(SceneManager.GetActiveScene().name != "Level-1")
        {
            _uIRef = GameObject.Find("Canvas").GetComponent<UIController>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _cpControllerRef.DeactivateCheckpoints();
            _spriteRendererRef.sprite = onCheckpoint;

            _cpControllerRef.SetSpawnPoint(transform.position);

            if (SceneManager.GetActiveScene().name != "Level-1" && !_isActive)
            {
                collision.GetComponent<PlayerHealthController>().currentHealth = collision.GetComponent<PlayerHealthController>().maxHealth;
                _uIRef.UpdateHealth();
            }
            else if(!_isActive)
            {
                collision.GetComponent<LevelOnePHC>().currentHealth = collision.GetComponent<LevelOnePHC>().maxHealth;
            }
            _isActive = true;
        }
    }

    public void ResetCheckPoint()
    {
        _spriteRendererRef.sprite = offCheckpoint;
    }
}
