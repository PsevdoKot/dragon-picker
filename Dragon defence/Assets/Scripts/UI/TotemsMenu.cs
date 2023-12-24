using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TotemsMenu : MonoBehaviour
{
    private TotemType currentTabType;
    [SerializeField] private TotemTabData[] tabDatas;

    void OnEnable()
    {
        foreach (var tabData in tabDatas)
        {
            tabData.button.onClick.AddListener(() => HandleButtonClick(tabData.type));
        }
    }

    void OnDisable()
    {
        foreach (var tabData in tabDatas)
        {
            tabData.button.onClick.RemoveAllListeners();
        }
    }

    private void HandleButtonClick(TotemType type)
    {
        if (type == currentTabType) return;

        var currentTabData = tabDatas.First(tabData => tabData.type == currentTabType);
        currentTabData.totemTabGO.SetActive(false);
        currentTabData.button.enabled = true;

        var newTabData = tabDatas.First(tabData => tabData.type == type);
        newTabData.totemTabGO.SetActive(true);
        newTabData.button.enabled = false;

        currentTabType = type;
    }
}
