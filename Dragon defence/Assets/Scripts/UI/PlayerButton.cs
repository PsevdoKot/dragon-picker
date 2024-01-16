using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static PlayerButton Instance;

    private string initialText;
    private TextMeshProUGUI buttonTextGUI;
    private Image buttonImage;
    private Color hideColor = new Color(255f, 255f, 255f, 0);
    private Color showColor = new Color(255f, 255f, 255f, 1f);

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonTextGUI = GetComponentInChildren<TextMeshProUGUI>();

        initialText = buttonTextGUI.text;
        Hide();
    }

    public void SelectAddShieldTarget()
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
        {
            targeting.EndTargetSelection(Player.Instance.gameObject);
            AudioManager.Instance.Play("totem-target-select");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.AirClick);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.Air);
        }
    }

    public void Hide()
    {
        buttonImage.color = hideColor;
        buttonTextGUI.text = "";
    }

    public void Show()
    {
        buttonImage.color = showColor;
        buttonTextGUI.text = initialText;
    }
}
