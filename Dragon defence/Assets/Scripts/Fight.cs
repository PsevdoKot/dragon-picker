using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using YG;

public class Fight : MonoBehaviour
{
    public static Fight Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textGUI;

    [SerializeField] private string winText;
    [SerializeField] private string defeatText;
    [SerializeField] private float timeForWin = 5f;
    [SerializeField] private float timeForDefeat = 5f;

    void Start()
    {
        Instance = this;

        textGUI.text = "";

        StartCoroutine(PlayAudio());
    }

    void Update()
    {

    }

    private IEnumerator PlayAudio()
    {
        AudioManager.Instance.Play("fight-start1");
        yield return new WaitForSecondsRealtime(3f);

        AudioManager.Instance.Play("fight-music");
    }

    public IEnumerator PlayerWin()
    {
        //LoadDataToSave(... + 10);
        //YandexGame.NewLeaderboardScores("TopPlayersScore", YandexGame.savesData.playerScore);
        textGUI.text = winText;
        Player.Instance.HandlePlayerWin();
        AudioManager.Instance.Stop("fight-music");
        AudioManager.Instance.Play("player-win");
        yield return new WaitForSecondsRealtime(timeForWin);

        SceneManager.LoadScene("Menu");
    }

    public IEnumerator PlayerDefeat()
    {
        //LoadDataToSave(... + 1);
        //YandexGame.NewLeaderboardScores("TopPlayersScore", YandexGame.savesData.playerScore);
        textGUI.text = defeatText;
        Dragon.Instance.HandlePlayerDefeat();
        AudioManager.Instance.Stop("fight-music");
        AudioManager.Instance.Play("player-lose1");
        yield return new WaitForSecondsRealtime(timeForDefeat);

        SceneManager.LoadScene("Menu");
    }
}
