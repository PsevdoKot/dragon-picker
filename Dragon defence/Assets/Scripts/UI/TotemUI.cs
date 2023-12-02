using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TotemUI : MonoBehaviour
{
    private int placeId;
    private Totem totem;
    [SerializeField] private Image health;
    [SerializeField] private Image shield;
    [SerializeField] private TextMeshProUGUI cooldownText;

    public void Init(int placeId, Totem totem)
    {
        this.placeId = placeId;
        this.totem = totem;
    }

    void Start()
    {

    }

    void Update()
    {
        if (placeId.IsUnityNull()) return;

        if (totem.IsUnityNull() || totem.HP <= 0)
        {
            Destroy(gameObject);
            return;
        }

        health.fillAmount = totem.HP / totem.maxHP;

        if (totem.actionTimer > 0)
        {
            cooldownText.gameObject.SetActive(true);
            cooldownText.text = $"{Mathf.Round(totem.actionTimer)}s";
        }
        else
        {
            cooldownText.gameObject.SetActive(false);
        }

        if (totem.shildAmount > 0)
        {
            shield.gameObject.SetActive(true);
            shield.fillAmount = totem.shildAmount / totem.maxShildAmount;
        }
        else
        {
            shield.gameObject.SetActive(false);
        }
    }
}
