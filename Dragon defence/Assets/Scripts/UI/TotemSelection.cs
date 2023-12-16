using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TotemSelection : MonoBehaviour
{
    public static TotemSelection Instance { get; private set; }

    private ITotemSelectionButton[] buttons;
    [SerializeField] private TextMeshProUGUI manaCostTextGUI;

    private bool isSelecting = false;
    private int currentTotemPlaceId;
    [SerializeField] private Vector3 textOffset;

    void Start()
    {
        Instance = this;

        buttons = GetComponentsInChildren<ITotemSelectionButton>();

        ToggleButtons(false);
        manaCostTextGUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isSelecting)
        {
            var currentMana = Player.Instance.mana;
            foreach (var button in buttons)
            {
                button.UpdateEnabled(currentMana);
            }
        }
    }

    private void ToggleButtons(bool state)
    {
        isSelecting = state;

        foreach (var button in buttons)
        {
            button.ToggleButton(state);
        }
    }

    public void HandleButtonPointerEnter(Vector3 buttonPos, Color buttonColor)
    {
        manaCostTextGUI.gameObject.SetActive(true);
        manaCostTextGUI.rectTransform.anchoredPosition = buttonPos + textOffset;
        manaCostTextGUI.color = buttonColor;
    }

    public void HandleButtonPointerExit()
    {
        manaCostTextGUI.gameObject.SetActive(false);
    }

    public void StartTotemSelection(int totemPlaceId)
    {
        currentTotemPlaceId = totemPlaceId;
        ToggleButtons(true);
    }

    public void CancelTotemSelection()
    {
        ToggleButtons(false);
    }

    public void EndTotemSelection(TotemType type)
    {
        TotemsRow.Instance.AddTotem(type, currentTotemPlaceId);
        ToggleButtons(false);
    }
}
