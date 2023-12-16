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
            _ => EarthTotem.ManaCost,
        };

        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateEnabled(float currentMana)
    {
        var enabled = currentMana > totemManaCost;
        button.enabled = enabled;
        buttonImage.color = enabled ? activeButtonColor : disabledButtonColor;
    }

    public void ToggleButton(bool state)
    {
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
        TotemSelection.Instance.EndTotemSelection(buttonType);

        AudioManager.Instance.Play("in-fight-click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TotemSelection.Instance.HandleButtonPointerEnter(rectTransform.anchoredPosition, buttonColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TotemSelection.Instance.HandleButtonPointerExit();
    }
}
