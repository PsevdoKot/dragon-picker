using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;

    [SerializeField] private TextMeshProUGUI scoreTextGUI;

    void OnEnable() => YandexGame.GetDataEvent += LoadDataFromSave;
    void OnDisable() => YandexGame.GetDataEvent -= LoadDataFromSave;

    void Awake()
    {
        Instance = this;
        Invoke("LoadDataFromSave", 4f);
    }

    private void LoadDataFromSave()
    {
        try
        {
            if (YandexGame.SDKEnabled)
            {
                scoreTextGUI.text = PlayerScoreManager.Instance.GetPlayerScore().ToString();
            }
        }
        catch
        {
            Debug.LogWarning("Не удалось загрузить количество очков");
        }
    }

    public void UpdateScoreAmount()
    {
        LoadDataFromSave();
    }
}
