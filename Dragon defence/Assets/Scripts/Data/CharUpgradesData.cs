using UnityEngine;

[System.Serializable]
public class CharUpgradesData
{
    public UpgradeType upgradeType;
    public UpgradeData[] upgradesData;
    [HideInInspector] public int maxUpgradesCount => upgradesData.Length;
}
