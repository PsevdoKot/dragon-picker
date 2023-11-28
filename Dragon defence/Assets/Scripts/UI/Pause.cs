using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool paused;
    [SerializeField] public GameObject pausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                paused = false;
                pausePanel.SetActive(false);
            }
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
