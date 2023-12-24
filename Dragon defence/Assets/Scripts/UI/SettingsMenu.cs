using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject mainMenuGO;

    void Start()
    {
        SetSliders();
    }

    void SetSliders()
    {
        masterSlider.value = YandexGame.savesData.masterVolume;
        sfxSlider.value = YandexGame.savesData.sfxVolume;
        musicSlider.value = YandexGame.savesData.musicVolume;
    }

    public void UpdateMasterVolume()
    {
        mixer.SetFloat("MasterVolume", masterSlider.value);
        YandexGame.savesData.masterVolume = masterSlider.value;
    }

    public void UpdateMusicVolume()
    {
        mixer.SetFloat("MusicVolume", musicSlider.value);
        YandexGame.savesData.musicVolume = musicSlider.value;
    }

    public void UpdateSFXVolume()
    {
        mixer.SetFloat("SFXVolume", sfxSlider.value);
        YandexGame.savesData.sfxVolume = sfxSlider.value;
    }

    public void CloseSettings()
    {
        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        gameObject.SetActive(false);
        mainMenuGO.SetActive(true);
    }
}
