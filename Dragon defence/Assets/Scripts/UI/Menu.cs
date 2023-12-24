using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject loadingInfoGO;
    [SerializeField] private GameObject mapMenuGO;
    [SerializeField] private GameObject totemsMenuGO;

    void Start()
    {
        AudioManager.Instance.Play("menu-music");
    }

    public void ToggleMapMenu()
    {
        // CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        if (mapMenuGO.activeInHierarchy)
        {
            mapMenuGO.SetActive(false);
        }
        else
        {
            totemsMenuGO.SetActive(false);
            mapMenuGO.SetActive(true);
        }
    }

    public void ToggleTotemsMenu()
    {
        // CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        if (totemsMenuGO.activeInHierarchy)
        {
            totemsMenuGO.SetActive(false);
        }
        else
        {
            mapMenuGO.SetActive(false);
            totemsMenuGO.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        mapMenuGO.SetActive(false);
        totemsMenuGO.SetActive(false);
        loadingInfoGO.SetActive(true);
        SceneManager.LoadSceneAsync("MainMenu");
    }

    void OnDestroy()
    {
        AudioManager.Instance.Stop("menu-music");
    }
}
