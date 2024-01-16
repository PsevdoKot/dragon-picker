using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool isPaused;
    private Button menuButton;
    [SerializeField] public GameObject pauseBackgroundGO;
    [SerializeField] public GameObject pauseTextGO;
    [SerializeField] public GameObject menuButtonGO;
    [SerializeField] private GameObject loadingPanelGO;

    void Awake()
    {
        menuButton = menuButtonGO.GetComponent<Button>();
    }

    void OnEnable()
    {
        menuButton.onClick.AddListener(LoadMenu);
    }

    void OnDisable()
    {
        menuButton.GetComponent<Button>().onClick.AddListener(LoadMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                Toggle(true);
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;
                Toggle(false);
            }
        }
    }

    private void Toggle(bool state)
    {
        pauseBackgroundGO.SetActive(state);
        pauseTextGO.SetActive(state);
        menuButtonGO.SetActive(state);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
		AudioManager.Instance.Stop("fight-music");
        AudioManager.Instance.Play("menu-click");
        loadingPanelGO.SetActive(true);
        SceneManager.LoadSceneAsync("Menu");
    }
}
