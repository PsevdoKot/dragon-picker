using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class FightParamsManager : MonoBehaviour
{
    public static FightParamsManager Instance;

    private int selectedRoadMapStep;
    private FightParamsData fightParams;
    [SerializeField] private bool setDafaultParams = false;

    void Awake()
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

    public void SetFightParams(int roadMapStep, FightParamsData fightParams)
    {
        if (fightParams == null) return;

        this.fightParams = fightParams;
        selectedRoadMapStep = roadMapStep;
    }

    public void PrepareFight()
    {
        if (fightParams == null) return;

        Fight.RoadMapStep = selectedRoadMapStep;
        Fight.WinScoreReward = fightParams.winScoreReward;
        Fight.DefeatScoreReward = fightParams.defeatScoreReward;
        PreparePlayer();
        PrepareDragons();
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
        Fireball.DragonFireballMinDamage = fightParams.dragonFireballMinDamage;
        Fireball.DragonFireballMaxDamage = fightParams.dragonFireballMaxDamage;
        Fireball.DragonFireballSpeed = fightParams.dragonFireballSpeed;
    }
}
