using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float blinkTime;
    [SerializeField] private int valueTostartBlink;
    [SerializeField] private int valueToStopBlink;
    [SerializeField] private GameObject icon;
    [SerializeField] private AudioSource _audioSource;
    
    private bool _isBlinking;

    private void Start()
    {
        GameEvents.Instance.OnUpdateTurboSpeed += SetValue;
    }

    private void Update()
    {
        if (slider.value < valueTostartBlink && !_isBlinking)
        {
            _isBlinking = true;
            StartCoroutine(Blink());
        }
        
        if(slider.value > valueToStopBlink && _isBlinking)
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
            _audioSource.Play();
            yield return new WaitForSeconds(blinkTime);
            icon.SetActive(true);
            yield return new WaitForSeconds(blinkTime);
        }
        
    }



}
