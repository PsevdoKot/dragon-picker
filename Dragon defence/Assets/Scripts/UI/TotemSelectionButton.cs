using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TotemSelectionButton : MonoBehaviour, ITotemSelectionButton, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Image buttonImage;
    private RectTransform rectTransform;

    private bool isActive;
    private bool isEnabled; // можно ли кликнуть
    private int totemManaCost;
    private Color activeButtonColor = new Color(255, 255, 255);
    private Color hideButtonColor = new Color(255, 255, 255, 0f);
    [SerializeField] private Color buttonColor;
    [SerializeField] private Color disabledButtonColor;
    [SerializeField] private TotemType buttonType;

    void Awake()
    {
        totemManaCost = buttonType switch
        {
            TotemType.Water => WaterTotem.ManaCost,
            TotemType.Fire => FireTotem.ManaCost,
            TotemType.Air => AirTotem.ManaCost,
            TotemType.Earth => EarthTotem.ManaCost,
            _ => throw new System.Exception("The new totem button has not been processed"),
        };

        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateEnabled(float currentMana)
    {
        isEnabled = currentMana > totemManaCost;
        button.enabled = isEnabled;
        buttonImage.color = isEnabled ? activeButtonColor : disabledButtonColor;
    }

    public void ToggleButton(bool state)
    {
        isActive = state;
        button.enabled = state;
        buttonImage.color = state ? activeButtonColor : hideButtonColor;

        if (state)
        {
            button.onClick.AddListener(HandleClick);
        }
        else
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void HandleClick()
    {
        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        TotemSelection.Instance.EndTotemSelection(buttonType);

        AudioManager.Instance.Play("in-fight-click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActive) return;

        if (isEnabled)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.StandartClick);
        }

        TotemSelection.Instance.HandleButtonPointerEnter(rectTransform.anchoredPosition, buttonColor, totemManaCost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isActive) return;

        if (isEnabled)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        }

        TotemSelection.Instance.HandleButtonPointerExit();
    }
}
