using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject PlayerPrefab;
    
    private bool gameStarted;
    
    private int playerScore;
    private float playerSpeed;
    private float currentTime;

    private int scoreRecord;
    private float speedRecord;
    private float timeRecord;
    
    private void Start()
    {
        GameEvents.Instance.OnItemCollide += UpdateScore;
        GameEvents.Instance.OnStartGame += SummonerPlayer;
        GameEvents.Instance.OnStartGame += StartGame;
        GameEvents.Instance.OnGameOver += StopGame;
    }

    private void Update()
    {
        PlayTimer(gameStarted);
    }

    private void StartGame()
    {
        gameStarted = true;
        playerScore = 0;
        playerSpeed = 0;
        currentTime = 0;
        GameEvents.Instance.ScreenUpdate(playerScore, playerSpeed);
    }

    private void StopGame(bool gameOver)
    {
        gameStarted = false;
        CheckRecords(gameOver);
    }
    
    private void PlayTimer(bool start)
    {
        if (start)
        {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = "Time: " + time.ToString(@"mm\:ss\:fff");
    }

    private void UpdateScore(int score, float speed)
    {
        playerScore += score;
        playerSpeed += speed;
        GameEvents.Instance.ScreenUpdate(playerScore, playerSpeed);
    }

    public void SummonerPlayer()
    {
        Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
    }

    private void CheckRecords(bool gameOver)
    {
        bool isScoreRecord = false; 
        bool isSpeedRecord = false;
        bool isTimeRecord = false;
        scoreRecord = PlayerPrefs.GetInt("score");
        speedRecord = PlayerPrefs.GetFloat("speed");
        timeRecord = PlayerPrefs.GetFloat("time");
        
        if (playerScore > scoreRecord)
        {
            PlayerPrefs.SetInt("score", playerScore);
            isScoreRecord = true;
        }

        if (playerSpeed > speedRecord)
        {
            PlayerPrefs.SetFloat("speed", playerSpeed);
            isSpeedRecord = true;
        }
        
        if (currentTime > timeRecord)
        {
            PlayerPrefs.SetFloat("time", currentTime);
            isTimeRecord = true;
        }

        if (isScoreRecord || isSpeedRecord || isTimeRecord)
        {
            print(currentTime + " - " + timeRecord);
            GameEvents.Instance.ShowNewRecord(isScoreRecord, isSpeedRecord, isTimeRecord);
        }
    }
}
