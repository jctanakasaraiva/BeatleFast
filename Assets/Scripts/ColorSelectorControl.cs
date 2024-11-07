using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelectorControl : MonoBehaviour
{
    [SerializeField] private Slider sliderRed;
    [SerializeField] private Slider sliderGreen;
    [SerializeField] private Slider sliderBlue;
    [SerializeField] private SpriteRenderer leftWing;
    [SerializeField] private SpriteRenderer rightWing;

    private void Awake()
    {
        sliderRed.onValueChanged.AddListener(delegate {UpdatePlayerColor();});
        sliderGreen.onValueChanged.AddListener(delegate {UpdatePlayerColor();});
        sliderBlue.onValueChanged.AddListener(delegate {UpdatePlayerColor();});
        UpdatePlayerColor();
    }

    void UpdatePlayerColor()
    {
        
        float red = sliderRed.value;
        float green = sliderGreen.value;
        float blue = sliderBlue.value;
        var playerColor = new Color(red, green, blue,1);
        leftWing.color = playerColor;
        rightWing.color = playerColor;
        PlayerPrefs.SetFloat("red", red);
        PlayerPrefs.SetFloat("green", green);
        PlayerPrefs.SetFloat("blue", blue);
    }
}
