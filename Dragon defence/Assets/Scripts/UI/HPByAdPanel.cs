using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class HPByAdPanel : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button denyButton;

    [SerializeField] private int givenHPAmount;

    void OnEnable()
    {
        YandexGame.CloseVideoEvent += GiveReward;
        confirmButton.onClick.AddListener(HandleConfirmButtonClick);
        denyButton.onClick.AddListener(HandleDenyButtonClick);
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        YandexGame.CloseVideoEvent -= GiveReward;
        confirmButton.onClick.RemoveAllListeners();
        denyButton.onClick.RemoveAllListeners();
        Time.timeScale = 1;
    }

    private void HandleConfirmButtonClick()
    {
        YandexGame.RewVideoShow(0);
    }

    private void GiveReward()
    {
        Player.Instance.IncreaseHP(givenHPAmount);
        Destroy(gameObject);
    }

    private void HandleDenyButtonClick()
    {
        gameObject.SetActive(false);
    }
}
