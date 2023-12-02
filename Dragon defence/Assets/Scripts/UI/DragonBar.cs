using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragonBar : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image health;

    void Start()
    {
        if (Dragon.Instance.IsUnityNull())
        {
            Destroy(gameObject);
        }

        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Dragon.Instance.HP <= 0)
        {
            healthBar.SetActive(false);
            return;
        }
        else
        {
            healthBar.SetActive(true);
        }

        var dragonPosOnScreen = Camera.main.WorldToScreenPoint(Dragon.Instance.transform.position);
        rectTransform.anchoredPosition = new Vector3(dragonPosOnScreen.x, dragonPosOnScreen.y, 0);

        health.fillAmount = Dragon.Instance.HP / Dragon.Instance.maxHP;
    }
}
