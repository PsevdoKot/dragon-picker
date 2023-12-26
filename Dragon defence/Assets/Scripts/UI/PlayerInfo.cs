using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;

    [SerializeField] private TextMeshProUGUI playerNameTextGUI;
    [SerializeField] private TextMeshProUGUI scoreTextGUI;

    private void onEnable() => YandexGame.GetDataEvent += LoadDataFromSave;
    private void onDisable() => YandexGame.GetDataEvent -= LoadDataFromSave;

    void Awake()
    {
        Instance = this;
    }

    private void LoadDataFromSave()
    {
        Debug.Log(scoreTextGUI);
        Debug.Log(PlayerScoreManager.Instance);
        scoreTextGUI.text = PlayerScoreManager.Instance.GetPlayerScore().ToString();
        playerNameTextGUI.text = YandexGame.playerName;
    }

    public void UpdateScoreAmount()
    {
        scoreTextGUI.text = PlayerScoreManager.Instance.GetPlayerScore().ToString();
    }
}
