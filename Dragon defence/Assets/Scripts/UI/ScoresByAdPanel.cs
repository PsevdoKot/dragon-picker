using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ScoresByAdPanel : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button denyButton;

    [SerializeField] private int givenScoreAmount;

    void Start()
    {

    }

    void OnEnable()
    {
        confirmButton.onClick.AddListener(HandleConfirmButtonClick);
        denyButton.onClick.AddListener(HandleDenyButtonClick);
    }

    void OnDisable()
    {
        confirmButton.onClick.RemoveAllListeners();
        denyButton.onClick.RemoveAllListeners();
    }

    private void HandleConfirmButtonClick()
    {
        PlayerScoreManager.Instance.ChangePlayerScore(givenScoreAmount);
        PlayerInfo.Instance.UpdateScoreAmount();
        Destroy(gameObject);
    }

    private void HandleDenyButtonClick()
    {
        gameObject.SetActive(false);
    }
}
