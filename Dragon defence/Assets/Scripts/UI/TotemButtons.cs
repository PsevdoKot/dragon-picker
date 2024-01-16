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

    public int buttonsCount { get; private set; } = 6;

    [SerializeField] private Vector2 offset;

    private readonly Dictionary<int, float> xButtonPosByPlaceId = new()
        { {0, 475}, {1, 380}, {2, 285}, {3, 190}, {4, 95}, {5, 0} };

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

        // var buttonGO = Instantiate(buttonPrefab, transform); x -75 xsize 0.95
        // var rectTransform = buttonGO.GetComponent<RectTransform>();
        // var totemXLocalPos = TotemsRow.Instance.xTotemLocalPosByPlaceId[placeId];
        // var totemPos = TotemsRow.Instance.transform.position + new Vector3(totemXLocalPos, 0, 0);
        // var totemPosOnScreen = Camera.main.WorldToScreenPoint(totemPos);
        // rectTransform.anchoredPosition = new Vector3(totemPosOnScreen.x + offset.x, totemPosOnScreen.y + offset.y, 0);

        var buttonScript = buttonGO.GetComponent<TotemButton>();
        buttonScript.Init(placeId);
        buttons[placeId] = buttonScript;
    }

    public void HideButton(int placeId)
    {
        buttons[placeId].Hide();
        StartCoroutine(TotemsUI.Instance.CreateTotemUI(placeId));
    }

    public void ShowButton(int placeId)
    {
        buttons[placeId].Show();
        TotemsUI.Instance.RemoveTotemUI(placeId);
    }

    public void ActiveButton(int placeId)
    {
        buttons[placeId].Active();
    }
}
