using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerScoreManager : MonoBehaviour
{
    public static PlayerScoreManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public int GetPlayerScore()
    {
        return YandexGame.savesData.totalScore;
    }

    public void ChangePlayerScore(int changeAmount)
    {
        YandexGame.savesData.totalScore += changeAmount;
        YandexGame.NewLeaderboardScores("TOPPlayersScore", YandexGame.savesData.totalScore);
        YandexGame.SaveProgress();
    }
}
