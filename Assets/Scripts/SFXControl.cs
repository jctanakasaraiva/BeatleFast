using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControl : MonoBehaviour
{
    [SerializeField] private AudioSource gameOverAudioSource;
    [SerializeField] private AudioSource MusicAudioSource;
    [SerializeField] private AudioClip gameOverClip;
    void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
        GameEvents.Instance.OnChangeMusic += ChangeMusic;
    }

    private void GameOver(bool obj)
    {
        gameOverAudioSource.PlayOneShot(gameOverClip);
    }

    private void ChangeMusic(AudioClip clip)
    {
        MusicAudioSource.clip = clip;
        MusicAudioSource.Play();
    }
}
