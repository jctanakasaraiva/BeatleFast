using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsMenuCanvas;
    [SerializeField] private GameObject _newGameMenuCanvas;
    [SerializeField] private GameObject _gameOverMenuCanvas;
    

    [Header("First Selected Options")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _newGameMenuFirst;
    [SerializeField] private GameObject _gameOverMenuFirst;
    
    private bool isPaused;
    private bool isGameStarted;
    private void Start()
    {
        CloseAllMenus();
        GameEvents.Instance.GameOver(true);
        OpenNewGameMenu();
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
        OpenMainMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        CloseAllMenus();
    }

    #endregion

    #region Activate/Deactivate Menus

    private void CloseAllMenus()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(false);
        _newGameMenuCanvas.SetActive(false);
        _gameOverMenuCanvas.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OpenNewGameMenu()
    {
        isGameStarted = false;
        CloseAllMenus();
        _newGameMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_newGameMenuFirst);
            
    }

    private void OpenGameOverMenu(bool gameOverStatus)
    {
        isGameStarted = !gameOverStatus;
        CloseAllMenus();
        _gameOverMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_gameOverMenuFirst);
    }

    private void OpenMainMenu()
    {
        CloseAllMenus();
        _mainMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }

    private void OpenSettingsMenu()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);
    }
    
    #endregion

    #region Commom Menu Button Actions

    public void OnQuitPress()
    {
        Application.Quit();
    }

    #endregion

    #region Main Menu Button Actions

    public void OnSettingPress()
    {
        OpenSettingsMenu();
    }

    public void OnResumePress()
    {
        Unpause();
    }

    #endregion

    #region Settings Menu Button Actions

    public void OnSettingsBackPress()
    {
        OpenMainMenu();
    }

    #endregion

    #region New Game Button Actions

    public void OnNewGamePress()
    {
        isGameStarted = true;
        GameEvents.Instance.StartGame();
        CloseAllMenus();
    }

    #endregion

    #region Game Over Menu Actions

    #endregion
}
