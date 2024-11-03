using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _pauseMenuCanvas;
    [SerializeField] private GameObject _settingsMenuCanvas;
    [SerializeField] private GameObject _gameOverMenuCanvas;
    [SerializeField] private GameObject _creditMenuCanvas;
    [SerializeField] private GameObject _gamePadMenuCanvas;
    [SerializeField] private GameObject _keyboardMenuCanvas;
    [SerializeField] private GameObject _audioMenuCanvas;
    [SerializeField] private GameObject _newRecordMenuCanvas;
    [SerializeField] private GameObject _leaderboardMenuCanvas;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _pauseMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _gameOverMenuFirst;
    [SerializeField] private GameObject _creditMenuFirst;
    [SerializeField] private GameObject _gamePadMenuFirst;
    [SerializeField] private GameObject _keyboardMenuFirst;
    [SerializeField] private GameObject _audioMenuFirst;
    [SerializeField] private GameObject _newRecordMenuFirst;
    [SerializeField] private GameObject _leaderboardMenuFirst;

    [Header("New Record Menu Options")] 
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private TextMeshProUGUI playerNameText;

    [Header("Game Over Record Frame")] 
    [SerializeField] private TextMeshProUGUI recordFrameText;

    private int scoreRecord;
    private float SpeedRecord;
    private float timeRecord;

    private bool scoreIsRecord, speedIsRecord, timeIsRecord;
    
    private bool isPaused;
    private bool isGameStarted;
    private bool isGameOver;
    private void Start()
    {
        CloseAllMenus();
        OpenMainMenu();
        GameEvents.Instance.OnGameOver += OpenGameOverMenu;
        GameEvents.Instance.OnNewRecord += OpenNewRecordMenu;
    }

    private void Update()
    {
        if (InputManager.instance.MenuOpenCloseInput)
        {
            if (isGameStarted)
            {
                if (!isPaused)
                {
                    Pause();
                }
                else
                {
                    Unpause();
                }
            }
            
        }
    }
    
    #region Pause/Unpause

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        OpenPauseMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        CloseAllMenus();
    }

    #endregion

    #region Open/Close Menus

    private void CloseAllMenus()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(false);
        _pauseMenuCanvas.SetActive(false);
        _gameOverMenuCanvas.SetActive(false);
        _creditMenuCanvas.SetActive(false);
        _gamePadMenuCanvas.SetActive(false);
        _keyboardMenuCanvas.SetActive(false);
        _audioMenuCanvas.SetActive(false);
        _newRecordMenuCanvas.SetActive(false);
        _leaderboardMenuCanvas.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OpenMainMenu()
    {
        isGameStarted = false;
        CloseAllMenus();
        _mainMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    private void OpenGameOverMenu(bool gameOverStatus)
    {
        isGameOver = gameOverStatus;
        isGameStarted = !gameOverStatus;
        CloseAllMenus();
        _gameOverMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_gameOverMenuFirst);
    }

    private void OpenNewRecordMenu(bool isScoreRecord, bool isSpeedRecord, bool isTimeRecord)
    {
        CloseAllMenus();
        _newRecordMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_newRecordMenuFirst);

        scoreIsRecord = isScoreRecord;
        speedIsRecord = isSpeedRecord;
        timeIsRecord = isTimeRecord;
        int scoreRecord = PlayerPrefs.GetInt("score");
        float SpeedRecord = PlayerPrefs.GetFloat("speed");
        float TimeRecord = PlayerPrefs.GetFloat("time");
        TimeSpan time = TimeSpan.FromSeconds(TimeRecord);
        //recordText.text = "Score: " + scoreRecord + "\nSpeed: " + SpeedRecord ;
        
        if (isScoreRecord && isSpeedRecord && isTimeRecord)
        {
            recordText.text = "Score: " + scoreRecord + 
                              "\nSpeed: " + SpeedRecord.ToString("f1") +
                              "\nTime: " + time.ToString(@"mm\:ss\:fff");
        }
        else if (isScoreRecord && !isSpeedRecord && !isTimeRecord )
        {
            recordText.text = "Score: " + scoreRecord;
        }
        else if (!isScoreRecord && isSpeedRecord && !isTimeRecord )
        {
            recordText.text = "Speed: " + SpeedRecord.ToString("f1");
        }
        else if (!isScoreRecord && !isSpeedRecord && isTimeRecord )
        {
            recordText.text = "Time: " + time.ToString(@"mm\:ss\:fff");
        }

    }

    private void OpenPauseMenu()
    {
        scoreRecord = PlayerPrefs.GetInt("score");
        SpeedRecord = PlayerPrefs.GetFloat("speed");
        timeRecord = PlayerPrefs.GetFloat("player");
        var playerScore = PlayerPrefs.GetString("playerScore");
        var playerSpeed = PlayerPrefs.GetString("playerSpeed");
        var playerTime = PlayerPrefs.GetString("playerTime");
        recordFrameText.text = "Name: " + playerScore + "Score: " + scoreRecord + "\n" +
                               "Name: " + playerSpeed + "Speed: " + SpeedRecord + "\n" +
                               "Name: " + playerTime + "Time: " + timeRecord;
        CloseAllMenus();
        _pauseMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
    }

    private void OpenSettingsMenu()
    {
        CloseAllMenus();
        _settingsMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);
    }

    private void OpenCreditMenu()
    {
        CloseAllMenus();
        _creditMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_creditMenuFirst);
    }
    private void OpenGamePadMenu()
    {
        CloseAllMenus();
        _gamePadMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_gamePadMenuFirst);
    }
    
    private void OpenKeyboardMenu()
    {
        CloseAllMenus();
        _keyboardMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_keyboardMenuFirst);
    }

    private void OpenAudioMenu()
    {
        CloseAllMenus();
        _audioMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_audioMenuFirst);
    }
    
    #endregion

    #region General Menu Button Actions

    public void OnQuitPress()
    {
        Application.Quit();
    }

    #endregion

    #region Main Menu Button Actions
    public void OnResumePress()
    {
        Unpause();
    }
    public void GoToSettingsMenu()
    {
        OpenSettingsMenu();
    }
    #endregion

    #region Settings Menu Button Actions

    public void GoToPauseMenu()
    {
        if (isGameOver)
        {
            OpenGameOverMenu(true);
        }
        else
        {
            OpenPauseMenu();
        }
    }

    public void GoToAudioMenu()
    {
        OpenAudioMenu();
    }

    #endregion

    #region Settings Butons Action

    public void GoToGamePad()
    {
        OpenGamePadMenu();
    }
    public void GoToKeyboard()
    {
        OpenKeyboardMenu();
    }

    #endregion

    #region New Game Button Actions
    public void StartNewGame()
    {
        isGameOver = false;
        isGameStarted = true;
        GameEvents.Instance.StartGame();
        CloseAllMenus();
    }
    
    public void GotoCreditMenu()
    {
        OpenCreditMenu();
    }
    #endregion

    public void GoToMainMenu()
    {
        OpenMainMenu();
    }

    public void SaveNewRecord()
    {
        var playerName = playerNameText.text;
        if (scoreIsRecord)
        {
            PlayerPrefs.SetString("playerScore", playerName);
        }
        if (speedIsRecord)
        {
            PlayerPrefs.SetString("playerSpeed", playerName);
        }
        if (timeIsRecord)
        {
            PlayerPrefs.SetString("playerTime", playerName);
        }
    }

    public void ClearRecords()
    {
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetFloat("speed", 0);
        recordFrameText.text = "Score: " + scoreRecord + "\nSpeed: " + SpeedRecord;
        recordFrameText.GraphicUpdateComplete();
    }
}
