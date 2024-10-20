using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameScreen : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    void Start()
    {
        newGameButton.onClick.AddListener(NewGame);
    }

    private void NewGame()
    {
        GameEvents.Instance.StartGame();
        Destroy(gameObject);
    }
}
