using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    private int playerScore;
    private int playerSpeed;

    private void Start()
    {
        GameEvents.Instance.OnItemCollide += UpdateScore;
        GameEvents.Instance.OnStartGame += SummonerPlayer;
        GameEvents.Instance.OnStartGame += StartGame;
    }

    private void StartGame()
    {
        playerScore = 0;
        playerSpeed = 0;
        GameEvents.Instance.ScreenUpdate(playerScore, playerSpeed);
    }

    private void UpdateScore(int score, int speed)
    {
        playerScore += score;
        playerSpeed += speed;
        GameEvents.Instance.ScreenUpdate(playerScore, playerSpeed);
    }

    public void SummonerPlayer()
    {
        Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
    }
}
