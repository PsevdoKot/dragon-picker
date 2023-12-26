using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class PlayerCharsManager : MonoBehaviour
{
    public static PlayerCharsManager Instance;

    public PlayerCharacteristics playerChars { get; private set; }

    [SerializeField] private TotemUpgradesData[] totemsUpgradesData;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(gameObject);
        }

        playerChars = new();
        CalculatePlayerCharsWithUpgrades();
    }

    public void SetPlayerCharacteristics()
    {
        if (playerChars == null) return;

        PrepareWaterTotems();
        PrepareFireTotems();
        PrepareAirTotems();
        PrepareEarthTotems();
    }

    private void PrepareWaterTotems()
    {
        WaterTotem.ManaCost = playerChars.waterTotemManaCost;
        WaterTotem.MaxHP = playerChars.waterTotemMaxHP;
        WaterTotem.TimeBetweenActions = playerChars.waterTotemTimeBetweenActions;
        WaterTotem.ManaRegenAmount = playerChars.waterTotemManaRegenAmount;
    }

    private void PrepareFireTotems()
    {
        FireTotem.ManaCost = playerChars.fireTotemManaCost;
        FireTotem.MaxHP = playerChars.fireTotemMaxHP;
        FireTotem.TimeBetweenActions = playerChars.fireTotemTimeBetweenActions;
        Fireball.TotemFireballMinDamage = playerChars.totemFireballDamage;
        Fireball.TotemFireballMaxDamage = playerChars.totemFireballDamage;
        Fireball.TotemFireballSpeed = playerChars.totemFireballSpeed;
    }

    private void PrepareAirTotems()
    {
        AirTotem.ManaCost = playerChars.airTotemManaCost;
        AirTotem.MaxHP = playerChars.airTotemMaxHP;
        AirTotem.TimeBetweenActions = playerChars.airTotemTimeBetweenActions;
        AirTotem.ShieldDuration = playerChars.shieldDuration;
        AirTotem.ActionShieldAmount = playerChars.shieldAmount;
    }

    private void PrepareEarthTotems()
    {
        EarthTotem.ManaCost = playerChars.earthTotemManaCost;
        EarthTotem.MaxHP = playerChars.earthTotemMaxHP;
        EarthTotem.TimeBetweenActions = playerChars.earthTotemTimeBetweenActions;
        EarthTotem.SlowDownDuration = playerChars.slowDownDuration;
        EarthTotem.SlowDownStrength = playerChars.slowDownStrength;
    }




#nullable enable
    public (float, float?, int?) GetTotalCharData(TotemType totemType, UpgradeType upgradeType)
    {
        var currentUpgradeData = GetNextUpgradeData(totemType, upgradeType);
        var charValue = GetPlayerCharValue(totemType, upgradeType);
        return (charValue, currentUpgradeData?.upgradeAmount, currentUpgradeData?.price);
    }

    private void CalculatePlayerCharsWithUpgrades()
    {
        foreach (var totemUpgradesData in totemsUpgradesData)
        {
            foreach (var charUpgradesData in totemUpgradesData.charUpgradesData)
            {
                var currentUpgrade = GetCurrentUpgrade(totemUpgradesData.totemType, charUpgradesData.upgradeType);
                for (var i = 0; i < charUpgradesData.maxUpgradesCount; i++)
                {
                    if (i > currentUpgrade) break;
                    ChangePlayerCrarValue(totemUpgradesData.totemType, charUpgradesData.upgradeType,
                        charUpgradesData.upgradesData[i].upgradeAmount);
                }
            }
        }
    }

    // возвращает null, в случае если апргейдов больше нет
    private UpgradeData? GetNextUpgradeData(TotemType totemType, UpgradeType upgradeType)
    {
        var currentUpgrade = GetCurrentUpgrade(totemType, upgradeType);
        var charUpgradesData = totemsUpgradesData
            .First(totemUpgradesData => totemUpgradesData.totemType == totemType)
            .charUpgradesData
            .First(charUpgradeData => charUpgradeData.upgradeType == upgradeType);
        return currentUpgrade + 1 == charUpgradesData.maxUpgradesCount
            ? null
            : charUpgradesData.upgradesData[currentUpgrade + 1];
    }

    private int GetCurrentUpgrade(TotemType totemType, UpgradeType upgradeType)
    {
        return totemType switch
        {
            TotemType.Water => upgradeType switch
            {
                UpgradeType.waterTotemManaCost => YandexGame.savesData.waterTotemManaCostUpgrade,
                UpgradeType.waterTotemMaxHP => YandexGame.savesData.waterTotemMaxHPUpgrade,
                UpgradeType.waterTotemTimeBetweenActions => YandexGame.savesData.waterTotemTimeBetweenActionsUpgrade,
                UpgradeType.waterTotemManaRegenAmount => YandexGame.savesData.waterTotemManaRegenAmountUpgrade,
                _ => throw new Exception("The new water totem upgrades has not been processed"),
            },
            TotemType.Fire => upgradeType switch
            {
                UpgradeType.fireTotemManaCost => YandexGame.savesData.fireTotemManaCostUpgrade,
                UpgradeType.fireTotemMaxHP => YandexGame.savesData.fireTotemMaxHPUpgrade,
                UpgradeType.fireTotemTimeBetweenActions => YandexGame.savesData.fireTotemTimeBetweenActionsUpgrade,
                UpgradeType.totemFireballDamage => YandexGame.savesData.totemFireballDamageUpgrade,
                UpgradeType.totemFireballSpeed => YandexGame.savesData.totemFireballSpeedUpgrade,
                _ => throw new Exception("The new fire totem upgrades has not been processed"),
            },
            TotemType.Air => upgradeType switch
            {
                UpgradeType.airTotemManaCost => YandexGame.savesData.airTotemManaCostUpgrade,
                UpgradeType.airTotemMaxHP => YandexGame.savesData.airTotemMaxHPUpgrade,
                UpgradeType.airTotemTimeBetweenActions => YandexGame.savesData.airTotemTimeBetweenActionsUpgrade,
                UpgradeType.shieldAmount => YandexGame.savesData.shieldAmountUpgrade,
                UpgradeType.shieldDuration => YandexGame.savesData.shieldDurationUpgrade,
                _ => throw new Exception("The new air totem upgrades has not been processed"),
            },
            TotemType.Earth => upgradeType switch
            {
                UpgradeType.earthTotemManaCost => YandexGame.savesData.earthTotemManaCostUpgrade,
                UpgradeType.earthTotemMaxHP => YandexGame.savesData.earthTotemMaxHPUpgrade,
                UpgradeType.earthTotemTimeBetweenActions => YandexGame.savesData.earthTotemTimeBetweenActionsUpgrade,
                UpgradeType.slowDownDuration => YandexGame.savesData.slowDownDurationUpgrade,
                UpgradeType.slowDownStrength => YandexGame.savesData.slowDownStrengthUpgrade,
                _ => throw new Exception("The new earth totem upgrades has not been processed"),
            },
            _ => throw new Exception("The new totem upgrades has not been processed"),
        };
    }

    public void UpgradeChar(TotemType totemType, UpgradeType upgradeType)
    {
        var nextUpgradeData = GetNextUpgradeData(totemType, upgradeType);
        if (nextUpgradeData == null) return;

        ChangePlayerCrarValue(totemType, upgradeType, nextUpgradeData.upgradeAmount);
        PlayerScoreManager.Instance.ChangePlayerScore(-nextUpgradeData.price);

        switch (totemType)
        {
            case TotemType.Water:
                switch (upgradeType)
                {
                    case UpgradeType.waterTotemManaCost:
                        YandexGame.savesData.waterTotemManaCostUpgrade++;
                        break;
                    case UpgradeType.waterTotemMaxHP:
                        YandexGame.savesData.waterTotemMaxHPUpgrade++;
                        break;
                    case UpgradeType.waterTotemTimeBetweenActions:
                        YandexGame.savesData.waterTotemTimeBetweenActionsUpgrade++;
                        break;
                    case UpgradeType.waterTotemManaRegenAmount:
                        YandexGame.savesData.waterTotemManaRegenAmountUpgrade++;
                        break;
                }
                break;
            case TotemType.Fire:
                switch (upgradeType)
                {
                    case UpgradeType.fireTotemManaCost:
                        YandexGame.savesData.fireTotemManaCostUpgrade++;
                        break;
                    case UpgradeType.fireTotemMaxHP:
                        YandexGame.savesData.fireTotemMaxHPUpgrade++;
                        break;
                    case UpgradeType.fireTotemTimeBetweenActions:
                        YandexGame.savesData.fireTotemTimeBetweenActionsUpgrade++;
                        break;
                    case UpgradeType.totemFireballDamage:
                        YandexGame.savesData.totemFireballDamageUpgrade++;
                        break;
                    case UpgradeType.totemFireballSpeed:
                        YandexGame.savesData.totemFireballSpeedUpgrade++;
                        break;
                }
                break;
            case TotemType.Air:
                switch (upgradeType)
                {
                    case UpgradeType.airTotemManaCost:
                        YandexGame.savesData.airTotemManaCostUpgrade++;
                        break;
                    case UpgradeType.airTotemMaxHP:
                        YandexGame.savesData.airTotemMaxHPUpgrade++;
                        break;
                    case UpgradeType.airTotemTimeBetweenActions:
                        YandexGame.savesData.airTotemTimeBetweenActionsUpgrade++;
                        break;
                    case UpgradeType.shieldAmount:
                        YandexGame.savesData.shieldAmountUpgrade++;
                        break;
                    case UpgradeType.shieldDuration:
                        YandexGame.savesData.shieldDurationUpgrade++;
                        break;
                }
                break;
            case TotemType.Earth:
                switch (upgradeType)
                {
                    case UpgradeType.earthTotemManaCost:
                        YandexGame.savesData.earthTotemManaCostUpgrade++;
                        break;
                    case UpgradeType.earthTotemMaxHP:
                        YandexGame.savesData.earthTotemMaxHPUpgrade++;
                        break;
                    case UpgradeType.earthTotemTimeBetweenActions:
                        YandexGame.savesData.earthTotemTimeBetweenActionsUpgrade++;
                        break;
                    case UpgradeType.slowDownDuration:
                        YandexGame.savesData.slowDownDurationUpgrade++;
                        break;
                    case UpgradeType.slowDownStrength:
                        YandexGame.savesData.slowDownStrengthUpgrade++;
                        break;
                }
                break;
        }

        YandexGame.SaveProgress();
    }

    private float GetPlayerCharValue(TotemType totemType, UpgradeType upgradeType)
    {
        return totemType switch
        {
            TotemType.Water => upgradeType switch
            {
                UpgradeType.waterTotemManaCost => playerChars.waterTotemManaCost,
                UpgradeType.waterTotemMaxHP => playerChars.waterTotemMaxHP,
                UpgradeType.waterTotemTimeBetweenActions => playerChars.waterTotemTimeBetweenActions,
                UpgradeType.waterTotemManaRegenAmount => playerChars.waterTotemManaRegenAmount,
                _ => throw new Exception("The new water totem upgrades has not been processed"),
            },
            TotemType.Fire => upgradeType switch
            {
                UpgradeType.fireTotemManaCost => playerChars.fireTotemManaCost,
                UpgradeType.fireTotemMaxHP => playerChars.fireTotemMaxHP,
                UpgradeType.fireTotemTimeBetweenActions => playerChars.fireTotemTimeBetweenActions,
                UpgradeType.totemFireballDamage => playerChars.totemFireballDamage,
                UpgradeType.totemFireballSpeed => playerChars.totemFireballSpeed,
                _ => throw new Exception("The new fire totem upgrades has not been processed"),
            },
            TotemType.Air => upgradeType switch
            {
                UpgradeType.airTotemManaCost => playerChars.airTotemManaCost,
                UpgradeType.airTotemMaxHP => playerChars.airTotemMaxHP,
                UpgradeType.airTotemTimeBetweenActions => playerChars.airTotemTimeBetweenActions,
                UpgradeType.shieldAmount => playerChars.shieldAmount,
                UpgradeType.shieldDuration => playerChars.shieldDuration,
                _ => throw new Exception("The new air totem upgrades has not been processed"),
            },
            TotemType.Earth => upgradeType switch
            {
                UpgradeType.earthTotemManaCost => playerChars.earthTotemManaCost,
                UpgradeType.earthTotemMaxHP => playerChars.earthTotemMaxHP,
                UpgradeType.earthTotemTimeBetweenActions => playerChars.earthTotemTimeBetweenActions,
                UpgradeType.slowDownDuration => playerChars.slowDownDuration,
                UpgradeType.slowDownStrength => playerChars.slowDownStrength,
                _ => throw new Exception("The new earth totem upgrades has not been processed"),
            },
            _ => throw new Exception("The new totem upgrades has not been processed"),
        };
    }

    private void ChangePlayerCrarValue(TotemType totemType, UpgradeType upgradeType, float changeValue)
    {
        switch (totemType)
        {
            case TotemType.Water:
                switch (upgradeType)
                {
                    case UpgradeType.waterTotemManaCost:
                        playerChars.waterTotemManaCost += (int)changeValue;
                        break;
                    case UpgradeType.waterTotemMaxHP:
                        playerChars.waterTotemMaxHP += (int)changeValue;
                        break;
                    case UpgradeType.waterTotemTimeBetweenActions:
                        playerChars.waterTotemTimeBetweenActions += changeValue;
                        break;
                    case UpgradeType.waterTotemManaRegenAmount:
                        playerChars.waterTotemManaRegenAmount += changeValue;
                        break;
                }
                break;
            case TotemType.Fire:
                switch (upgradeType)
                {
                    case UpgradeType.fireTotemManaCost:
                        playerChars.fireTotemManaCost += (int)changeValue;
                        break;
                    case UpgradeType.fireTotemMaxHP:
                        playerChars.fireTotemMaxHP += (int)changeValue;
                        break;
                    case UpgradeType.fireTotemTimeBetweenActions:
                        playerChars.fireTotemTimeBetweenActions += changeValue;
                        break;
                    case UpgradeType.totemFireballDamage:
                        playerChars.totemFireballDamage += (int)changeValue;
                        break;
                    case UpgradeType.totemFireballSpeed:
                        playerChars.totemFireballSpeed += changeValue;
                        break;
                }
                break;
            case TotemType.Air:
                switch (upgradeType)
                {
                    case UpgradeType.airTotemManaCost:
                        playerChars.airTotemManaCost += (int)changeValue;
                        break;
                    case UpgradeType.airTotemMaxHP:
                        playerChars.airTotemMaxHP += (int)changeValue;
                        break;
                    case UpgradeType.airTotemTimeBetweenActions:
                        playerChars.airTotemTimeBetweenActions += changeValue;
                        break;
                    case UpgradeType.shieldAmount:
                        playerChars.shieldAmount += (int)changeValue;
                        break;
                    case UpgradeType.shieldDuration:
                        playerChars.shieldDuration += changeValue;
                        break;
                }
                break;
            case TotemType.Earth:
                switch (upgradeType)
                {
                    case UpgradeType.earthTotemManaCost:
                        playerChars.earthTotemManaCost += (int)changeValue;
                        break;
                    case UpgradeType.earthTotemMaxHP:
                        playerChars.earthTotemMaxHP += (int)changeValue;
                        break;
                    case UpgradeType.earthTotemTimeBetweenActions:
                        playerChars.earthTotemTimeBetweenActions += changeValue;
                        break;
                    case UpgradeType.slowDownDuration:
                        playerChars.slowDownDuration += changeValue;
                        break;
                    case UpgradeType.slowDownStrength:
                        playerChars.slowDownStrength += changeValue;
                        break;
                }
                break;
        }
    }
}
