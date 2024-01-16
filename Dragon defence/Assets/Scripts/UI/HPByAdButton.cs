using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HPByAdButton : MonoBehaviour
{
    private Button button;
    private Image image;
    [SerializeField] private GameObject hpByAdPanelGO;

    private bool wasActivated;

    void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    void OnEnable()
    {
        button.onClick.AddListener(HandleClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    void Update()
    {
        if (hpByAdPanelGO.IsUnityNull()) // после одного просмотра рекламы убираем кнопку
        {
            Destroy(gameObject);
            return;
        }

        if (Player.Instance.HP < 0)
        {
            Destroy(hpByAdPanelGO);
            Destroy(gameObject);
            return;
        }

        var enabled = ((float)Player.Instance.HP / Player.MaxHP) < 0.2f;
        button.enabled = enabled;
        image.enabled = enabled;
    }

    private void HandleClick()
    {
        hpByAdPanelGO.SetActive(!hpByAdPanelGO.activeInHierarchy);
    }
}
