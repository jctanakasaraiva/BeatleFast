using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action OnStartGame;
    public void StartGame() => OnStartGame?.Invoke();

    public event Action<int, float> OnItemCollide;
    public void ItemCollide(int itemScore, float itemSpeed) => OnItemCollide?.Invoke(itemScore, itemSpeed);

    public event Action<int, float> OnScreenUpdate;
    public void ScreenUpdate(int updateScore, float updateSpeed) => OnScreenUpdate?.Invoke(updateScore, updateSpeed);

    public event Action<bool> OnGameOver;
    public void GameOver(bool gameOver) => OnGameOver?.Invoke(gameOver);

    public event Action<Vector3> OnPlayerMove;
    public void PlayerMove(Vector3 playerPosition) => OnPlayerMove?.Invoke(playerPosition);

    public event Action<float> OnUpdateTurboSpeed;
    public void UpdateTurboSpeed(float speedValue) => OnUpdateTurboSpeed?.Invoke(speedValue);

    public event Action<bool, bool, bool> OnNewRecord;
    public void ShowNewRecord(bool scoreRecord, bool speedRecord, bool timeRecord) => OnNewRecord?.Invoke(scoreRecord, speedRecord, timeRecord);


}
