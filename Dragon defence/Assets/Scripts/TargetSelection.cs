using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour
{
    public static TargetSelection Instance { get; private set; }

    public bool isOccupied { get; private set; }
    public TotemType initiatorType { get; private set; }

    private Totem currentInitiator;
    public int initiatorPlaceId;

    void Start()
    {
        Instance = this;
    }

    private void SaveData(Totem initiator)
    {
        isOccupied = true;
        currentInitiator = initiator;
        initiatorType = initiator.type;
        initiatorPlaceId = initiator.placeId;
    }

    public void StartTargetSelection(Totem initiator)
    {
        if (isOccupied) return;

        SaveData(initiator);
        CursorManager.Instance.ChangeCursorType(initiator.type);
    }

    public void InterruptTargetSelection(int placeId)
    {
        if (initiatorPlaceId == placeId)
        {
            isOccupied = false;
            CursorManager.Instance.ChangeCursorType(TotemType.Water);
        }
    }

    public void EndTargetSelection(GameObject target)
    {
        isOccupied = false;
        currentInitiator.Action(target);
        CursorManager.Instance.ChangeCursorType(TotemType.Water);
    }
}
