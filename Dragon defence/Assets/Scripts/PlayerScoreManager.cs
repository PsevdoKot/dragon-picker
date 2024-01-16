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
        YandexGame.savesData.totalScore = (int)Mathf.Clamp(GetPlayerScore(), 0, Mathf.Infinity);
        YandexGame.SaveProgress();
		if (YandexGame.auth)
        {
            YandexGame.NewLeaderboardScores("TOPPlayersScore", YandexGame.savesData.totalScore);
        }
    }
}
