using System;
using System.Collections;
using System.Collections.Generic;
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

    [Header("First Selected Options")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _pauseMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _gameOverMenuFirst;
    [SerializeField] private GameObject _creditMenuFirst;
    [SerializeField] private GameObject _gamePadMenuFirst;
    [SerializeField] private GameObject _keyboardMenuFirst;
    
    private bool isPaused;
    private bool isGameStarted;
    private bool isGameOver;
    private void Start()
    {
        GameEvents.Instance.GameOver(true);
        CloseAllMenus();
        OpenMainMenu();
        GameEvents.Instance.OnGameOver += OpenGameOverMenu;
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

    private void OpenPauseMenu()
    {
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
}
