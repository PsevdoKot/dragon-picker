using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonButton : MonoBehaviour
{
    private RectTransform rectTransform;

    // void Start()
    // {
    //     rectTransform = GetComponent<RectTransform>();
    // }

    // void Update()
    // {
    //     var dragonPosOnScreen = Camera.main.WorldToScreenPoint(Dragon.Instance.transform.position);
    //     rectTransform.anchoredPosition = new Vector3(dragonPosOnScreen.x, dragonPosOnScreen.y, 0);
    // }

    public void SelectSlowDownTarget()
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Earth)
        {
            TargetSelection.Instance.EndTargetSelection(Dragon.Instance.gameObject);
            AudioManager.Instance.Play("totem-target-selection");
        }
    }
}
