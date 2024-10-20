using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int ItemScoreValue;
    [SerializeField] private int ItemSpeedValue;

    private void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameOver -= GameOver;
    }

    private void GameOver(bool gameOver)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameEvents.Instance.ItemCollide(ItemScoreValue, ItemSpeedValue); 
        Destroy(gameObject);
    }
}
