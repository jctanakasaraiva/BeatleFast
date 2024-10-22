using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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
}
