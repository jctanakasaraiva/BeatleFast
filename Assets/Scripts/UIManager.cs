using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject newGameScreen;

    private void Start()
    {
        GameEvents.Instance.OnScreenUpdate += UpdateScreen;
        //GameEvents.Instance.OnGameOver += ShowGameOverScreen;
        //ShowNewGameScreen();
    }
    
    private void UpdateScreen(int score, float speed)
    {
        scoreText.text = "Score: " + score.ToString();
        speedText.text = "Speed: " + speed.ToString();
    }

    private void ShowGameOverScreen(bool gameStatus)
    {
        Instantiate(gameOverScreen, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    private void ShowNewGameScreen()
    {
        Instantiate(newGameScreen, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
    }
}
