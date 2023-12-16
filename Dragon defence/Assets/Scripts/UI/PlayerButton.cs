using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    public void SelectAddShieldTarget()
    {
        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Air)
        {
            targeting.EndTargetSelection(Player.Instance.gameObject);
        }
    }
}
