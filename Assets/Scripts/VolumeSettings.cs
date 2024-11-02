using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sFXSlider;
    [SerializeField] private Slider playerSlider;
    [SerializeField] private Slider enemySlider;
    [SerializeField] private Slider itemSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterAudio();
            SetSFXAudio();
            SetPlayerAudio();
            SetEnemyAudio();
            SetItemsAudio();
        }
    }

    public void SetMasterAudio()
    {
        float volume = masterSlider.value;
        mixer.SetFloat("master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetSFXAudio()
    {
        float volume = sFXSlider.value;
        mixer.SetFloat("sfx", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void SetPlayerAudio()
    {
        float volume = playerSlider.value;
        mixer.SetFloat("player", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("playerVolume", volume);
    }
    public void SetEnemyAudio()
    {
        float volume = enemySlider.value;
        mixer.SetFloat("enemy", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("enemyVolume", volume);
    }
    public void SetItemsAudio()
    {
        float volume = itemSlider.value;
        mixer.SetFloat("items", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("itemsVolume", volume);
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        sFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        playerSlider.value = PlayerPrefs.GetFloat("playerVolume");
        enemySlider.value = PlayerPrefs.GetFloat("enemyVolume");
        itemSlider.value = PlayerPrefs.GetFloat("itemsVolume");
        
        SetMasterAudio();
        SetSFXAudio();
        SetPlayerAudio();
        SetEnemyAudio();
        SetItemsAudio();

    }
}
