using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;

public class TotemChar : MonoBehaviour
{
    private TextMeshProUGUI upgradeButtonTextGUI;
    private Image upgradeButtonImage;
    [SerializeField] private TextMeshProUGUI charAmountTextGUI;
    [SerializeField] private TextMeshProUGUI upgradeAmountTextGUI;
    [SerializeField] private TextMeshProUGUI upgradePriceTextGUI;
    [SerializeField] private Button upgradeButton;

    private Color standartColor = new Color(1f, 1f, 1f);
    private Color disabledColor = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    [SerializeField] private TotemType totemType;
    [SerializeField] private UpgradeType upgradeType;

    void Start()
    {
        upgradeButtonTextGUI = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        upgradeButtonImage = upgradeButton.GetComponentInChildren<Image>();
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
            upgradeButtonTextGUI.text = "X";

            upgradeButton.enabled = false;
            upgradeButtonImage.color = disabledColor;

            CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        }
        else
        {
            var upgradeAmount = (float)totalCharData.Item2;
            upgradeAmountTextGUI.text = $"{(upgradeAmount > 0 ? '+' : '-')}{Mathf.Abs(upgradeAmount)}";
            upgradePriceTextGUI.text = totalCharData.Item3.ToString();

            var enabled = totalCharData.Item3 <= PlayerScoreManager.Instance.GetPlayerScore();
            upgradeButton.enabled = enabled;
            upgradeButtonImage.color = enabled ? standartColor : disabledColor;
        }
    }

    private void HandleButtonClick()
    {
        PlayerCharsManager.Instance.UpgradeChar(totemType, upgradeType);
        UpdateCharInfo();
        PlayerInfo.Instance.UpdateScoreAmount();
    }
}
