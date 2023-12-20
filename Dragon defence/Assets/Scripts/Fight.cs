using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using YG;
using System.Linq;

public class Fight : MonoBehaviour
{
    public static Fight Instance { get; private set; }

    public static DragonType DragonType { get; set; } = DragonType.Usurper;
    public static CharacterType CharacterType { get; set; } = CharacterType.Male;

    [SerializeField] private TextMeshProUGUI textGUI;


    [SerializeField] private string winText;
    [SerializeField] private string defeatText;
    [SerializeField] private float timeForWin = 5f;
    [SerializeField] private float timeForDefeat = 5f;
    [SerializeField] private Vector3 dragonStartPos = new(0, -10.9f, -17f);
    [SerializeField] private Vector3 characterPos = new(-2.2f, -10.5f, 6f);
    [SerializeField] private Quaternion dragonRotation = new(0, 0, 0, 0);
    [SerializeField] private Quaternion characterRotation = new(0, 0, 0, 0);
    [SerializeField] private DragonData[] dragonDatas;
    [SerializeField] private CharacterData[] characterDatas;

    void Start()
    {
        Instance = this;

        textGUI.text = "";

        SetPlayer();
        SetDragon();

        StartCoroutine(PlayAudio());
    }

    private void SetPlayer()
    {
        var character = Instantiate(characterDatas.First((data) => data.type == CharacterType).prefab,
            characterPos, characterRotation);
    }

    private void SetDragon()
    {
        var dragon = Instantiate(dragonDatas.First((data) => data.type == DragonType).prefab,
            dragonStartPos, dragonRotation);
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
