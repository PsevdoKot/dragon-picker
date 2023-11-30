using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Fight : MonoBehaviour
{
    public static Fight Instance;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {

    }

    public void PlayerWin()
    {
        //LoadDataToSave(... + 10);
        //YandexGame.NewLeaderboardScores("TopPlayersScore", YandexGame.savesData.playerScore);
        SceneManager.LoadScene("Menu");
    }

    public void PlayerLose()
    {
        //LoadDataToSave(... + 1);
        //YandexGame.NewLeaderboardScores("TopPlayersScore", YandexGame.savesData.playerScore);
        SceneManager.LoadScene("Menu");
    }
}
