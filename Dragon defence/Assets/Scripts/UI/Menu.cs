using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void LoadFight()
    {
        SceneManager.LoadScene("DragonFight");
    }

    public void OpenInventory()
    {

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
