using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private int ballSpeed;
    private void Start()
    {
        GameEvents.Instance.OnGameOver += GameOver;
        GetComponent<Rigidbody2D>().AddForce(transform.right * ballSpeed);
        Destroy(gameObject, 8);
    }   
    private void OnDestroy()
    {
        GameEvents.Instance.OnGameOver -= GameOver;
    }
    private void GameOver(bool gameOver)
    {
        Destroy(gameObject);
    }
}
