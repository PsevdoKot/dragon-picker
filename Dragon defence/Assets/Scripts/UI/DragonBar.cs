using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragonBar : MonoBehaviour
{
    private Dragon dragon;
    private RectTransform rectTransform;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image health;

    [SerializeField] private Vector3 offset = new(-440f, -220f);

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        dragon = Dragon.Instance;

        if (dragon.HP <= 0)
        {
            healthBar.SetActive(false);
            return;
        }
        else
        {
            healthBar.SetActive(true);
        }

        var dragonPosOnScreen = Camera.main.WorldToScreenPoint(dragon.transform.position);
        rectTransform.anchoredPosition = new Vector3(dragonPosOnScreen.x + offset.x, dragonPosOnScreen.y + offset.y, 0);

        health.fillAmount = (float)dragon.HP / Dragon.MaxHP;
    }
}
