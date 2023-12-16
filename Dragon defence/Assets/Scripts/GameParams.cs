using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParams : MonoBehaviour
{
    // Это объект для передачи данных из сцены "Menu" в сцену "Fight"

    public static GameParams Instance;

    //// <-- На будущее
    //// public int maxTotemsCount;
    //// public ... location;
    //// public ... dayTime;

    // тип перса получаем из YandexGame/WorkingData/SavesYG
    // public int playerMaxMana;
    // public int playerMaxHP;

    //// public int dragonCount;
    // public GameObject dragonPrefab; //????
    // public int dragonMaxHP;
    // public int dragonSpeed;
    // public int dragonAttackSpeed;
    // public float dragonFireballDamage;

    // public float waterTotemManaCost;
    // public int waterTotemMaxHP;
    // public float waterTotemTimeBetweenActions;
    // public float manaRegenAmount;

    // public float fireTotemManaCost;
    // public int fireTotemMaxHP;
    // public float fireTotemTimeBetweenActions;
    // public float totemFireballDamage;

    // public float airTotemManaCost;
    // public int airTotemMaxHP;
    // public float airTotemTimeBetweenActions;
    // public float shieldDuration;
    // public float shieldAmount;

    // public float earthTotemManaCost;
    // public int earthTotemMaxHP;
    // public float earthTotemTimeBetweenActions;
    // public float slowDownDuration;
    // public float slowDownStrength;


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }

        // if (SceneManager.GetActiveScene().name == "DragonFight")
        // {
        //     PrepareFight();
        // }
    }

    // public void PrepareFight()
    // {

    // }
}
