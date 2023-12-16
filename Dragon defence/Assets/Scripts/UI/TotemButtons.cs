using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TotemButtons : MonoBehaviour
{
    public static TotemButtons Instance;

    private Button[] buttons;
    private Dictionary<int, Image> buttonsImageByPlaceId = new();
    private Dictionary<int, TextMeshProUGUI> buttonsTextByPlaceId = new();
    private float initialButtonAlpha;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] public int buttonsCount { get; private set; } = 6;

    private readonly Dictionary<int, float> xButtonPosByPlaceId = new()
        { {0, 260}, {1, 160}, {2, 60}, {3, -40}, {4, -140}, {5, -240} };

    void Start()
    {
        Instance = this;

        buttons = new Button[buttonsCount];
        for (var i = 0; i < buttonsCount; i++)
        {
            CreateButton(i);
        }
        initialButtonAlpha = buttons[0].GetComponent<Image>().color.a;
    }

    void Update()
    {

    }

    private void CreateButton(int placeId)
    {
        var buttonGO = Instantiate(buttonPrefab, transform);
        buttonGO.transform.localPosition = new Vector3(xButtonPosByPlaceId[placeId], 0, 0);

        var button = buttonGO.GetComponent<Button>();
        button.onClick.AddListener(() => HandleButtonClick(placeId));
        buttons[placeId] = button;

        buttonsImageByPlaceId.Add(placeId, button.GetComponent<Image>());
        buttonsTextByPlaceId.Add(placeId, buttonGO.GetComponentInChildren<TextMeshProUGUI>());
    }

    private void HandleButtonClick(int placeId)
    {
        if (!Player.Instance.isActive) return;

        var totem = TotemsRow.Totems[placeId];
        if (totem.IsUnityNull())
        {
            TotemSelection.Instance.StartTotemSelection(placeId);
            AudioManager.Instance.Play("in-fight-click");
        }
        else
        {
            var targeting = TargetSelection.Instance;
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

    public void HandleTotemAppearance(int placeId)
    {
        var buttonImage = buttonsImageByPlaceId[placeId];
        buttonImage.color = buttonImage.color.WithAlpha(0);
        buttonsTextByPlaceId[placeId].gameObject.SetActive(false);
        TotemsUI.Instance.CreateTotemUI(placeId);
    }

    public void HandleTotemDestroy(int placeId)
    {
        var buttonImage = buttonsImageByPlaceId[placeId];
        buttonImage.color = buttonImage.color.WithAlpha(initialButtonAlpha);
        buttonsTextByPlaceId[placeId].gameObject.SetActive(true);
        TotemsUI.Instance.RemoveTotemUI(placeId);
    }

    void OnDestroy()
    {
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
