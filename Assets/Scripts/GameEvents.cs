using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action OnStartGame;
    public void StartGame() => OnStartGame?.Invoke();

    public event Action<int, int> OnItemCollide;
    public void ItemCollide(int itemScore, int itemSpeed) => OnItemCollide?.Invoke(itemScore, itemSpeed);

    public event Action<int, int> OnScreenUpdate;
    public void ScreenUpdate(int updateScore, int updateSpeed) => OnScreenUpdate?.Invoke(updateScore, updateSpeed);

    public event Action<bool> OnGameOver;
    public void GameOver(bool gameOver) => OnGameOver?.Invoke(gameOver);



}
