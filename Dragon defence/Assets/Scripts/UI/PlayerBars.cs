using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    [SerializeField] private Image mana;
    [SerializeField] private Image health;

    void Start()
    {
        
    }

    void Update()
    {
        mana.fillAmount = Player.Instance.mana / Player.Instance.maxMana;
        health.fillAmount = Player.Instance.HP / Player.Instance.maxHP;
    }
}
