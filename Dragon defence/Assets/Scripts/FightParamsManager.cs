using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightParamsManager : MonoBehaviour
{
    public static FightParamsManager Instance;

    private static FightParams currentFightParams;
    private static List<IFightObject> currentFightObjects;

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

        currentFightObjects = new();
    }

    public static void SubscribeOnFightLoad(IFightObject fightObject)
    {
        currentFightObjects.Add(fightObject);
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "DragonFight" && currentFightParams != null)
        {
            PrepareFight();
        }
    }

    public static void SetFightParams(FightParams fightParams)
    {
        currentFightParams = fightParams;
    }

    public static void PrepareFight()
    {
        foreach (var fightObject in currentFightObjects)
        {
            fightObject.SetFightParams(currentFightParams);
        }
    }
}
