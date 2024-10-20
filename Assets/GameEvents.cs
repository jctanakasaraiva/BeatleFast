using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action OnDoorAreaEnter;
    public void DoorAreaEnter() => OnDoorAreaEnter?.Invoke();
    
    public event Action OnDoorAreaExit;
    public void DoorAreaExit() => OnDoorAreaExit?.Invoke();

    public event Action<int> OnScreenUpdate;
    public void ScreenUpdate(int score) => OnScreenUpdate?.Invoke(score);




}
