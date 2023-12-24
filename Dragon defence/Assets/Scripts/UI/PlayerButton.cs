using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void SelectAddShieldTarget()
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
        {
            targeting.EndTargetSelection(Player.Instance.gameObject);
            AudioManager.Instance.Play("totem-target-select");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.AirClick);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.Air);
        }
    }
}
