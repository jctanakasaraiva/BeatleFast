using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int ItemScoreValue;
    [SerializeField] private float ItemSpeedValue;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip spawnClip, destroyClip;
    [SerializeField] private float audioSourcePitch;
    
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GameEvents.Instance.OnGameOver += GameOver;
        _audioSource.pitch = audioSourcePitch;
        _audioSource.PlayOneShot(spawnClip);
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameOver -= GameOver;
    }

    private void GameOver(bool gameOver)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _audioSource.pitch = 1;
        _audioSource.PlayOneShot(destroyClip);
        GameEvents.Instance.ItemCollide(ItemScoreValue, ItemSpeedValue);
        _spriteRenderer.enabled = false;
        Destroy(gameObject,1f);
    }
}
