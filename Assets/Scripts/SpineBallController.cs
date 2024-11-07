using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineBallController : MonoBehaviour
{
    [SerializeField] private GameObject spineBall;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip slowClip, fastClip;

    private void Awake()
    {
        GameEvents.Instance.OnGamePaused += GamePaused;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGamePaused -= GamePaused;
    }

    private void GamePaused(bool isGamePaused)
    {
        _audioSource.mute = isGamePaused;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.clip = fastClip;
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.clip = slowClip;
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
    }
}
