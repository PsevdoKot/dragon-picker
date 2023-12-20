[System.Serializable]
public class FightParamsData
{
    // Объект для передачи данных из сцены "Menu" в сцену "Fight"

    //// <-- На будущее
    //// public int maxTotemsCount = 6;
    //// public ... location;
    //// public ... dayTime;

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

    public int waterTotemManaCost = 30;
    public int waterTotemMaxHP = 30;
    public float waterTotemTimeBetweenActions = 7f;
    public float waterTotemManaRegenAmount = 10;

    public int fireTotemManaCost = 40;
    public int fireTotemMaxHP = 30;
    public float fireTotemTimeBetweenActions = 5f;

    public int airTotemManaCost = 20;
    public int airTotemMaxHP = 30;
    public float airTotemTimeBetweenActions = 10f;
    public float shieldDuration = 15f;
    public int shieldAmount = 60;

    public int earthTotemManaCost = 20;
    public int earthTotemMaxHP = 30;
    public float earthTotemTimeBetweenActions = 20;
    public float slowDownDuration = 10;
    public float slowDownStrength = 2.5f;

    public int dragonFireballDamage = 20;
    public float dragonFireballSpeed = 0.5f;
    public int totemFireballDamage = 15;
    public float totemFireballSpeed = 0.5f;
}
