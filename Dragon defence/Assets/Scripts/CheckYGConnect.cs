using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CheckYGConnect : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += CheckSDK;
    private void OnDisable() => YandexGame.GetDataEvent -= CheckSDK;

    void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            CheckSDK();
        }
    }

    void Update()
    {

    }

    private void CheckSDK()
    {
        if (!YandexGame.auth)
        {
            YandexGame.AuthDialog();
        }

    }
}
