using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TotemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static TotemButtons Instance;

    private Button button;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;

    private Color initialColor;
    private Color hideColor;
    private string initialText;
    private int placeId;

    public void Init(int placeId)
    {
        this.placeId = placeId;
    }

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
        buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        initialColor = buttonImage.color;
        hideColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
        initialText = buttonText.text;
        button.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        if (!Player.Instance.isActive) return;

        var totem = TotemsRow.Totems[placeId];
        var targeting = TargetSelection.Instance;
        if (totem.IsUnityNull())
        {
            if (!targeting.isOccupied)
            {
                TotemSelection.Instance.StartTotemSelection(placeId);
                AudioManager.Instance.Play("in-fight-click");
            }
        }
        else
        {
            if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
            {
                targeting.EndTargetSelection(totem.gameObject);
                AudioManager.Instance.Play("totem-target-select");
            }
            else if (totem.isReady)
            {
                totem.PrepareAction();
                AudioManager.Instance.Play("in-fight-click");
            }
        }
    }

    public void Hide()
    {
        buttonImage.color = hideColor;
        buttonText.text = "";
    }

    public void Show()
    {
        buttonImage.color = initialColor;
        buttonText.text = initialText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        var totem = TotemsRow.Totems[placeId];
        if (totem.IsUnityNull())
        {
            if (!targeting.isOccupied)
            {
                CursorManager.Instance.ChangeCursorType(CursorType.StandartClick);
            }
        }
        else if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.AirClick);
        }
        else if (totem.isReady)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.StandartClick);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied)
        {
            if (targeting.initiatorType == TotemType.Air)
            {
                CursorManager.Instance.ChangeCursorType(CursorType.Air);
            }
        }
        else
        {
            CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        }
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
