using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void LoadMenu()
    {
        AudioManager.Instance.Play("menu-click");
        SceneManager.LoadSceneAsync("Menu");
    }

    public void Exit()
    {
        AudioManager.Instance.Play("menu-click");
        Application.Quit();
    }
}
