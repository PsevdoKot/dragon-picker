using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPByAdPanel : MonoBehaviour
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button denyButton;

    [SerializeField] private int givenHPAmount;

    void OnEnable()
    {
        confirmButton.onClick.AddListener(HandleConfirmButtonClick);
        denyButton.onClick.AddListener(HandleDenyButtonClick);
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        confirmButton.onClick.RemoveAllListeners();
        denyButton.onClick.RemoveAllListeners();
        Time.timeScale = 1;
    }

    private void HandleConfirmButtonClick()
    {
        Player.Instance.IncreaseHP(givenHPAmount);
        Destroy(gameObject);
    }

    private void HandleDenyButtonClick()
    {
        gameObject.SetActive(false);
    }
}
