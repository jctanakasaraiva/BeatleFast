using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        GameEvents.Instance.OnScreenUpdate += UpdateScreen;
    }
    
    private void UpdateScreen(int score)
    {
        text.text = score.ToString();
    }
}
