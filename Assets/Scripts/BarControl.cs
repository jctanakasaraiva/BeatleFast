using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject icon;
    
    private bool _isBlinking;

    private void Start()
    {
        GameEvents.Instance.OnUpdateTurboSpeed += SetValue;
    }

    private void Update()
    {
        if (slider.value < 10 && !_isBlinking)
        {
            _isBlinking = true;
            StartCoroutine(Blink());
        }
        
        if(slider.value > 60 && _isBlinking)
        {
            StopCoroutine(Blink());
            _isBlinking = false;
        }
    }

    private void OnEnable()
    {
        icon.SetActive(true);
    }

    private void SetValue(float value)
    {
        slider.value = value;
    }

    private IEnumerator Blink()
    {
        while (_isBlinking)
        {
            icon.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            icon.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        
    }



}
