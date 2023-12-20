using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotemButtons : MonoBehaviour
{
    public static TotemButtons Instance;

    private TotemButton[] buttons;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] public int buttonsCount { get; private set; } = 6;

    private readonly Dictionary<int, float> xButtonPosByPlaceId = new()
        { {0, 260}, {1, 160}, {2, 60}, {3, -40}, {4, -140}, {5, -240} };

    void Start()
    {
        Instance = this;

        buttons = new TotemButton[buttonsCount];
        for (var i = 0; i < buttonsCount; i++)
        {
            CreateButton(i);
        }
    }

    private void CreateButton(int placeId)
    {
        var buttonGO = Instantiate(buttonPrefab, transform);
        buttonGO.transform.localPosition = new Vector3(xButtonPosByPlaceId[placeId], 0, 0);

        var buttonScript = buttonGO.GetComponent<TotemButton>();
        buttonScript.Init(placeId);
        buttons[placeId] = buttonScript;
    }

    public void HideButton(int placeId)
    {
        buttons[placeId].Hide();
        TotemsUI.Instance.CreateTotemUI(placeId);
    }

    public void ShowButton(int placeId)
    {
        buttons[placeId].Show();
        TotemsUI.Instance.RemoveTotemUI(placeId);
    }
}
