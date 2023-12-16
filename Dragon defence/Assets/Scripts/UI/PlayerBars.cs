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

    void Start()
    {
        player = Player.Instance;
    }

    void Update()
    {
        manaAmountTextGUI.text = $"{(int)player.mana}/{player.maxMana}";
        mana.fillAmount = player.mana / player.maxMana;
        health.fillAmount = (float)player.HP / player.maxHP;
        shield.fillAmount = player.maxShield > 0 ? (float)player.shield / player.maxShield : 0;
    }
}
