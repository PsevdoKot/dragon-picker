[System.Serializable]
public class PlayerCharacteristics
{
    // Объект для передачи характеристик игрока из сцены "Menu" в сцену "Fight"
    // Полям присваиваются начальные значения, до апргейдов

    public int waterTotemManaCost = 30;
    public int waterTotemMaxHP = 20;
    public float waterTotemTimeBetweenActions = 7f;
    public float waterTotemManaRegenAmount = 10f;

    public int fireTotemManaCost = 40;
    public int fireTotemMaxHP = 15;
    public float fireTotemTimeBetweenActions = 5f;
    public int totemFireballDamage = 15;
    public float totemFireballSpeed = 0.5f;

    public int airTotemManaCost = 20;
    public int airTotemMaxHP = 25;
    public float airTotemTimeBetweenActions = 10f;
    public float shieldDuration = 12f;
    public int shieldAmount = 20;

    public int earthTotemManaCost = 20;
    public int earthTotemMaxHP = 20;
    public float earthTotemTimeBetweenActions = 20;
    public float slowDownDuration = 7f;
    public float slowDownStrength = 1.5f;
}