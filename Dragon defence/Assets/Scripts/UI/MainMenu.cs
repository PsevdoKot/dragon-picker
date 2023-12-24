using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject loadingInfoGO;
    [SerializeField] private GameObject settingsMenuGO;

    void Start()
    {
        LoadSettings();

        AudioManager.Instance.Play("main-menu-music");
    }

    private void LoadSettings()
    {
        mixer.SetFloat("MasterVolume", YandexGame.savesData.masterVolume);
        mixer.SetFloat("MusicVolume", YandexGame.savesData.musicVolume);
        mixer.SetFloat("SFXVolume", YandexGame.savesData.sfxVolume);
    }

    public void LoadMenu()
    {
        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        loadingInfoGO.SetActive(true);
        SceneManager.LoadSceneAsync("Menu");
    }
    public void OpenSettings()
    {
        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        gameObject.SetActive(false);
        settingsMenuGO.SetActive(true);
    }

    public void Exit()
    {
        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        Application.Quit();
    }

    void OnDestroy()
    {
        AudioManager.Instance.Stop("main-menu-music");
    }
}
