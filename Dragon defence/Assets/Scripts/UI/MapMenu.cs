using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using YG;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Color nextFightButtonColor = new Color(1f, 0.73f, 0.47f);

    void Awake()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];

            var nextFight = YandexGame.savesData.roadMapStep == i;
            if (nextFight)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "";
                button.GetComponentInChildren<Image>().color = nextFightButtonColor;
            }
            else
            {
                var isEnabled = YandexGame.savesData.roadMapStep > i;
                button.enabled = isEnabled;
                button.GetComponentInChildren<TextMeshProUGUI>().text = isEnabled ? "X" : "";
            }
        }
    }
}
