using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private AudioSource gameOverSound;
    private void Start()
    {
        restartButton.onClick.AddListener(RestartClicked);
        gameOverSound.Play();
    }

    private void RestartClicked()
    {
        Destroy(gameObject);
        GameEvents.Instance.StartGame();
    }
}
