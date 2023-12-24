using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;

public class TotemChar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI charAmountTextGUI;
    [SerializeField] private TextMeshProUGUI upgradeAmountTextGUI;
    [SerializeField] private TextMeshProUGUI upgradePriceTextGUI;
    [SerializeField] private Button upgradeButton;

    [SerializeField] private TotemType totemType;
    [SerializeField] private UpgradeType upgradeType;

    void Start()
    {
        UpdateCharInfo();
    }

    void OnEnable()
    {
        upgradeButton.onClick.AddListener(HandleButtonClick);
    }

    void OnDisable()
    {
        upgradeButton.onClick.RemoveAllListeners();
    }

    private void UpdateCharInfo()
    {
        var totalCharData = PlayerCharsManager.Instance.GetTotalCharData(totemType, upgradeType);

        charAmountTextGUI.text = totalCharData.Item1.ToString();

        if (totalCharData.Item2 == null)
        {
            upgradeAmountTextGUI.text = "";
            upgradePriceTextGUI.text = "";
            upgradeButton.enabled = false;
            upgradeButton.GetComponent<TextMeshProUGUI>().text = "✓"; // или "✔"
        }
        else
        {
            var upgradeAmount = (float)totalCharData.Item2;
            upgradeAmountTextGUI.text = $"{Mathf.Sign(upgradeAmount)}{Mathf.Abs(upgradeAmount)}";
            upgradePriceTextGUI.text = totalCharData.Item3.ToString();
            upgradeButton.enabled = totalCharData.Item3 <= YandexGame.savesData.totalScore;
        }
    }

    private void HandleButtonClick()
    {
        PlayerCharsManager.Instance.UpgradeChar(totemType, upgradeType);
        UpdateCharInfo();
    }
}
