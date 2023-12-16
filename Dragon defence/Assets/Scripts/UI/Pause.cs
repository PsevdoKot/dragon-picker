using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] public GameObject pauseBackground;
    [SerializeField] public GameObject pauseText;
    [SerializeField] public GameObject pauseButton;

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
        pauseBackground.SetActive(state);
        pauseText.SetActive(state);
        pauseButton.SetActive(state);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
