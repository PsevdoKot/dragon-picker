using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameTextGUI;
    [SerializeField] private TextMeshProUGUI scoreTextGUI;

    // private void onEnable() => YandexGame.GetDataEvent += LoadDataFromSave;
    // private void onDisable() => YandexGame.GetDataEvent -= LoadDataFromSave;

    void Start()
    {
        // playerNameTextGUI.text = YandexGame.playerName;
        scoreTextGUI.text = YandexGame.savesData.totalScore.ToString();

        // if (YandexGame.SDKEnabled)
        // {
        //     LoadDataFromSave();
        // }
    }

    public void LoadDataFromSave()
    {

    }

    public void LoadDataToSave(int score)
    {
        // YandexGame.savesData.totalScore = score;
        // YandexGame.SaveProgress();
    }
}
