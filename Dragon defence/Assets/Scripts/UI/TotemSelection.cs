using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotemSelection : MonoBehaviour
{
    public static TotemSelection Instance { get; private set; }

    [SerializeField] private Button waterBtn;
    [SerializeField] private Button fireBtn;
    [SerializeField] private Button airBtn;
    [SerializeField] private Button earthBtn;
    [SerializeField] private Button cancelBtn;

    private int currentTotemPlaceId;

    void Start()
    {
        Instance = this;
    }

    private void ShowButtons(bool state)
    {
        waterBtn.gameObject.SetActive(state);
        fireBtn.gameObject.SetActive(state);
        airBtn.gameObject.SetActive(state);
        earthBtn.gameObject.SetActive(state);
        cancelBtn.gameObject.SetActive(state);

        if (state)
        {
            waterBtn.onClick.AddListener(() => EndTotemSelection(TotemType.Water));
            fireBtn.onClick.AddListener(() => EndTotemSelection(TotemType.Fire));
            airBtn.onClick.AddListener(() => EndTotemSelection(TotemType.Air));
            earthBtn.onClick.AddListener(() => EndTotemSelection(TotemType.Earth));
            cancelBtn.onClick.AddListener(CancelTotemSelection);
        }
        else
        {
            waterBtn.onClick.RemoveAllListeners();
            fireBtn.onClick.RemoveAllListeners();
            airBtn.onClick.RemoveAllListeners();
            earthBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.RemoveAllListeners();
        }
    }

    public void StartTotemSelection(int totemPlaceId)
    {
        currentTotemPlaceId = totemPlaceId;
        ShowButtons(true);
    }

    private void CancelTotemSelection()
    {
        ShowButtons(false);
    }

    private void EndTotemSelection(TotemType type)
    {
        TotemsRow.Instance.AddTotem(type, currentTotemPlaceId);
        ShowButtons(false);
    }
}
