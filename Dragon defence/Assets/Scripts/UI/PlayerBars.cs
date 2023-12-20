using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBars : MonoBehaviour
{
    private Player player;
    [SerializeField] private Image mana;
    [SerializeField] private Image health;
    [SerializeField] private Image shield;
    [SerializeField] private TextMeshProUGUI manaAmountTextGUI;

    void Update()
    {
        player = Player.Instance;
        manaAmountTextGUI.text = $"{(int)player.mana}/{Player.MaxMana}";
        mana.fillAmount = player.mana / Player.MaxMana;
        health.fillAmount = (float)player.HP / Player.MaxHP;
        shield.fillAmount = player.maxShield > 0 ? (float)player.shield / player.maxShield : 0;
    }
}
