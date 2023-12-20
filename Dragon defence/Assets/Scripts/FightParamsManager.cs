using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class FightParamsManager : MonoBehaviour
{
    public static FightParamsManager Instance;

    private FightParamsData fightParams;
	[SerializeField] private bool setDafaultParams = false;

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
		
		if (setDafaultParams)
		{
			fightParams = new FightParamsData();
		}
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "DragonFight" && fightParams != null)
        {
            PrepareFight();
        }
    }

    public void SetFightParams(FightParamsData fightParams)
    {
		if (fightParams != null)
		{
			this.fightParams = fightParams;
		}
    }

    private void PrepareFight()
    {
        PreparePlayer();
        PrepareDragons();
        PrepareWaterTotems();
        PrepareFireTotems();
        PrepareAirTotems();
        PrepareEarthTotems();
        PrepareFireballs();
    }

    private void PreparePlayer()
    {
        Fight.CharacterType = YandexGame.savesData.playerCharacterType;
        Player.MaxMana = fightParams.playerMaxMana;
        Player.MaxHP = fightParams.playerMaxHP;
        Player.ManaRegenSpeed = fightParams.manaRegenSpeed;
    }

    private void PrepareDragons()
    {
        Fight.DragonType = fightParams.dragonType;
        Dragon.MaxHP = fightParams.dragonMaxHP;
        Dragon.Speed = fightParams.dragonSpeed;
        Dragon.XSpeed = fightParams.dragonXSpeed;
        Dragon.YSpeed = fightParams.dragonYSpeed;
        Dragon.AttackSpeed = fightParams.dragonAttackSpeed;
    }

    private void PrepareWaterTotems()
    {
        WaterTotem.ManaCost = fightParams.waterTotemManaCost;
        WaterTotem.MaxHP = fightParams.waterTotemMaxHP;
        WaterTotem.TimeBetweenActions = fightParams.waterTotemTimeBetweenActions;
        WaterTotem.ManaRegenAmount = fightParams.waterTotemManaRegenAmount;
    }

    private void PrepareFireTotems()
    {
        FireTotem.ManaCost = fightParams.fireTotemManaCost;
        FireTotem.MaxHP = fightParams.fireTotemMaxHP;
        FireTotem.TimeBetweenActions = fightParams.fireTotemTimeBetweenActions;
    }

    private void PrepareAirTotems()
    {
        AirTotem.ManaCost = fightParams.airTotemManaCost;
        AirTotem.MaxHP = fightParams.airTotemMaxHP;
        AirTotem.TimeBetweenActions = fightParams.airTotemTimeBetweenActions;
        AirTotem.ShieldDuration = fightParams.shieldDuration;
        AirTotem.ActionShieldAmount = fightParams.shieldAmount;
    }

    private void PrepareEarthTotems()
    {
        EarthTotem.ManaCost = fightParams.earthTotemManaCost;
        EarthTotem.MaxHP = fightParams.earthTotemMaxHP;
        EarthTotem.TimeBetweenActions = fightParams.earthTotemTimeBetweenActions;
        EarthTotem.SlowDownDuration = fightParams.slowDownDuration;
        EarthTotem.SlowDownStrength = fightParams.slowDownStrength;
    }

    private void PrepareFireballs()
    {
        Fireball.DragonFireballDamage = fightParams.dragonFireballDamage;
        Fireball.DragonFireballSpeed = fightParams.dragonFireballSpeed;
        Fireball.TotemFireballDamage = fightParams.totemFireballDamage;
        Fireball.TotemFireballSpeed = fightParams.totemFireballSpeed;
    }
}
