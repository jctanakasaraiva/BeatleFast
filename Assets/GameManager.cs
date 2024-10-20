using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;

    private void Start()
    {
        GameEvents.Instance.OnDoorAreaEnter += UpdateScore;
    }

    private void UpdateScore()
    {
        score++;
        GameEvents.Instance.ScreenUpdate(score);
    }
}
