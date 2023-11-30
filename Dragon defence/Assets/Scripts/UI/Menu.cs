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
        var scoreGO = GameObject.Find("Score");
        scoreText = scoreGO.GetComponent<TextMeshProUGUI>();

        if (YandexGame.SDKEnabled)
        {
            LoadDataFromSave();
        }
    }

    void Update()
    {

    }

    // public void PlayerLose()
    // {
    //     LoadDataToSave(int.Parse(scoreText.text));
    //     YandexGame.NewLeaderboardScores("TopPlayersScore", YandexGame.savesData.playerScore);
    //     SceneManager.LoadScene("Menu");
    // }

    public void LoadDataFromSave()
    {

    }

    public void LoadDataToSave(int score)
    {
        YandexGame.savesData.playerScore = score;
        YandexGame.SaveProgress();
    }

    public void LoadFight()
    {
        SceneManager.LoadScene("DragonFight");
    }

    // public void OpenInventory()
    // {

    // }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
