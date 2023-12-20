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

    private float initialAlpha;
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

        initialAlpha = buttonImage.color.a;
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
                AudioManager.Instance.Play("totem-target-selection");
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
        buttonImage.color = buttonImage.color.WithAlpha(0);
        buttonText.text = "";
    }

    public void Show()
    {
        buttonImage.color = buttonImage.color.WithAlpha(initialAlpha);
        buttonText.text = initialText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        var isPlaceEmpty = TotemsRow.Totems[placeId].IsUnityNull();
        if (isPlaceEmpty)
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
