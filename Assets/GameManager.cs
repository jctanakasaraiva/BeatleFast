using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    private int playerScore;
    private float playerSpeed;

    private int scoreRecord;
    private float speedRecord;

    private void Start()
    {
        GameEvents.Instance.OnItemCollide += UpdateScore;
        GameEvents.Instance.OnStartGame += SummonerPlayer;
        GameEvents.Instance.OnStartGame += StartGame;
        GameEvents.Instance.OnGameOver += CheckRecords;
    }

    private void StartGame()
    {
        playerScore = 0;
        playerSpeed = 0;
        GameEvents.Instance.ScreenUpdate(playerScore, playerSpeed);
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
        scoreRecord = PlayerPrefs.GetInt("score");
        speedRecord = PlayerPrefs.GetFloat("speed");
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

        if (isScoreRecord || isSpeedRecord)
        {
            GameEvents.Instance.ShowNewRecord(isScoreRecord, isSpeedRecord);
        }
    }
}
