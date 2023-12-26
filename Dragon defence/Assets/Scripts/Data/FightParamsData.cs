[System.Serializable]
public class FightParamsData
{
    // Объект для передачи параметров битвы из сцены "Menu" в сцену "Fight"

    //// <-- На будущее
    //// public ... location;
    //// public ... dayTime;

    public int winScoreReward = 10;
    public int defeatScoreReward = 1;

    //// public int maxTotemsCount = 6;
    // тип перса получаем из YandexGame/WorkingData/SavesYG
    public int playerMaxMana = 100;
    public int playerMaxHP = 100;
    public float manaRegenSpeed = 0.005f;

    //// public int dragonCount = 1;
    public DragonType dragonType = DragonType.Usurper;
    public int dragonMaxHP = 100;
    public float dragonSpeed = 4f;
    public float dragonXSpeed = 12f;
    public float dragonYSpeed = 6f;
    public float dragonAttackSpeed = 5f;

    public int dragonFireballMinDamage = 15;
    public int dragonFireballMaxDamage = 25;
    public float dragonFireballSpeed = 0.5f;
}
