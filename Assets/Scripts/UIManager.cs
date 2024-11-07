using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject inGameHud;
    
    private void Start()
    {
        GameEvents.Instance.OnScreenUpdate += UpdateScreen;
        GameEvents.Instance.OnStartGame += StartGame;
        GameEvents.Instance.OnGameOver += GameOver;
        GameEvents.Instance.OnGamePaused += HideInGameHud;
    }

    private void UpdateScreen(int score, float speed)
    {
        scoreText.text = "Score: " + score.ToString();
        speedText.text = "Speed: " + speed.ToString("f1");
    }

    private void StartGame()
    {
        ShowInGameHud();
    }

    private void GameOver(bool gameOver)
    {
        HideInGameHud(gameOver);
    }

    private void ShowInGameHud()
    {
        inGameHud.SetActive(true);
    }
    
    private void HideInGameHud(bool other)
    {
        inGameHud.SetActive(false);
    }
    
}
