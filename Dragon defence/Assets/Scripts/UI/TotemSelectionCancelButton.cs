using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TotemSelectionCancelButton : MonoBehaviour, ITotemSelectionButton
{
    private Button button;
    private Image buttonImage;

    private Color activeButtonColor = new Color(255, 255, 255);
    private Color hideButtonColor = new Color(255, 255, 255, 0f);

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
    }

    public void UpdateEnabled(float currentMana) { }

    public void ToggleButton(bool state)
    {
        button.enabled = state;
        buttonImage.color = state ? activeButtonColor : hideButtonColor;

        if (state)
        {
            button.onClick.AddListener(TotemSelection.Instance.CancelTotemSelection);
        }
        else
        {
            button.onClick.RemoveAllListeners();
        }
    }
}