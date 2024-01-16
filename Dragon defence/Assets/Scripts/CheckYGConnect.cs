using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CheckYGConnect : MonoBehaviour
{
    public static CheckYGConnect Instance;

    void Awake()
    {
        Instance = this;
    }

    // void Start()
    // {
    //     if (YandexGame.SDKEnabled)
    //     {
    //         ShowAuthDialog();
    //     }
    // }

    // public void ShowAuthDialog()
    // {
    //     if (YandexGame.SDKEnabled && !YandexGame.auth)
    //     {
    //         YandexGame.AuthDialog();
    //     }
    // }
}
