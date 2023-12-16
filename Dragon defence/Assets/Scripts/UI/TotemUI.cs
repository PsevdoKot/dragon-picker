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

        health.fillAmount = (float)totem.HP / totem.maxHP;
        shield.fillAmount = totem.maxShield > 0 ? (float)totem.shield / totem.maxShield : 0;

        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
        if (totem.actionTimer > 0)
        {
            cooldownText.gameObject.SetActive(true);
            cooldownText.text = $"{Mathf.Round(totem.actionTimer)}s";
        }
        else
        {
            cooldownText.gameObject.SetActive(false);
        }
    }
}
