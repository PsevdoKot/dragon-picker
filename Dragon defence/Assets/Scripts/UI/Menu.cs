using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using TMPro;

public class Menu : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void onEnable() => YandexGame.GetDataEvent += LoadDataFromSave;
    private void onDisable() => YandexGame.GetDataEvent -= LoadDataFromSave;

    void Start()
    {

        if (YandexGame.SDKEnabled)
        {
            LoadDataFromSave();
        }
    }

    void Update()
    {

    }

    public void LoadDataFromSave()
    {

    }

    public void LoadDataToSave(int score)
    {
        // YandexGame.savesData.totalScore = score;
        // YandexGame.SaveProgress();
    }

    public void LoadFight()
    {
        AudioManager.Instance.Play("menu-click");
        SceneManager.LoadSceneAsync("DragonFight");
    }

    // public void OpenInventory()
    // {

    // }

    public void LoadMainMenu()
    {
        AudioManager.Instance.Play("menu-click");
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
