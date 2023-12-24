using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragonButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void SelectSlowDownTarget()
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Earth)
        {
            TargetSelection.Instance.EndTargetSelection(Dragon.Instance.gameObject);
            AudioManager.Instance.Play("totem-target-select");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Earth)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.EarthClick);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Earth)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.Earth);
        }
    }
}
