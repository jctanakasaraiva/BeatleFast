using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControl : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip gameOverClip;
    void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
    }

    private void GameOver(bool obj)
    {
        _audioSource.PlayOneShot(gameOverClip);
    }
}
