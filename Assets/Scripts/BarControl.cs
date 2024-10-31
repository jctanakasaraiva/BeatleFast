using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        GameEvents.Instance.OnUpdateTurboSpeed += SetValue;
    }

    private void SetValue(float value)
    {
        slider.value = value;
    }
}
