[System.Serializable]
public class PlayerCharacteristics
{
    // Объект для передачи характеристик игрока из сцены "Menu" в сцену "Fight"
    // Полям присваиваются начальные значения, до апргейдов

    public int waterTotemManaCost = 30;
    public int waterTotemMaxHP = 30;
    public float waterTotemTimeBetweenActions = 7f;
    public float waterTotemManaRegenAmount = 10;

    public int fireTotemManaCost = 40;
    public int fireTotemMaxHP = 30;
    public float fireTotemTimeBetweenActions = 5f;
    public int totemFireballDamage = 15;
    public float totemFireballSpeed = 0.5f;

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
}