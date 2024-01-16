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

    public static int RoadMapStep { get; set; } = 0;
    public static int WinScoreReward { get; set; } = 10;
    public static int DefeatScoreReward { get; set; } = 1;
    public static DragonType DragonType { get; set; } = DragonType.Usurper;
    public static CharacterType CharacterType { get; set; } = CharacterType.Male;

    private TextMeshProUGUI fightInfoTextGUI;
    [SerializeField] private GameObject fightInfoGO;
    [SerializeField] private GameObject loadingPanelGO;

    [SerializeField] private string winText = "Вы победили";
    [SerializeField] private string defeatText = "Вы проиграли";
    [SerializeField] private float timeForMusicStart = 3f;
    [SerializeField] private float timeForWin = 5f;
    [SerializeField] private float timeForDefeat = 5f;
    [SerializeField] private Vector3 dragonStartPos = new(0, -10.9f, -17f);
    [SerializeField] private Vector3 characterPos = new(-2.2f, -10.5f, 6f);
    [SerializeField] private Quaternion dragonRotation = new(0, 0, 0, 0);
    [SerializeField] private Quaternion characterRotation = new(0, 0, 0, 0);
    [SerializeField] private DragonData[] dragonDatas;
    [SerializeField] private CharacterData[] characterDatas;

    void Awake()
    {
        FightParamsManager.Instance.PrepareFight();
        PlayerCharsManager.Instance.SetPlayerCharacteristics();
    }

    void Start()
    {
        Instance = this;

        fightInfoTextGUI = fightInfoGO.GetComponentInChildren<TextMeshProUGUI>();

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
        yield return new WaitForSecondsRealtime(timeForMusicStart);

        AudioManager.Instance.Play("fight-music");
    }

    public IEnumerator PlayerWin()
    {
        //LoadDataToSave(... + 10);
        //YandexGame.NewLeaderboardScores("TopPlayersScore", YandexGame.savesData.playerScore);
        fightInfoGO.SetActive(true);
        fightInfoTextGUI.text = winText;
        Player.Instance.HandlePlayerWin();
        AudioManager.Instance.Stop("fight-music");
        AudioManager.Instance.Play("player-win");
        yield return new WaitForSecondsRealtime(timeForWin);

        GiveRewardToPlayer(true);
        LoadMenu();
    }

    public IEnumerator PlayerDefeat()
    {
        //LoadDataToSave(... + 1);
        //YandexGame.NewLeaderboardScores("TopPlayersScore", YandexGame.savesData.playerScore);
        fightInfoGO.SetActive(true);
        fightInfoTextGUI.text = defeatText;
        Dragon.Instance.HandlePlayerDefeat();
        AudioManager.Instance.Stop("fight-music");
        AudioManager.Instance.Play("player-lose1");
        yield return new WaitForSecondsRealtime(timeForDefeat);

        GiveRewardToPlayer(false);
        LoadMenu();
    }

    private void LoadMenu()
    {
        loadingPanelGO.SetActive(true);
        SceneManager.LoadSceneAsync("Menu");
        // if (Random.Range(0, 3) == 0)
        // {
        //     YandexGame.FullscreenShow();
        // }
    }

    private void GiveRewardToPlayer(bool win)
    {
        if (win)
        {
            if (YandexGame.savesData.roadMapStep == RoadMapStep)
            {
                YandexGame.savesData.roadMapStep++; // сохранение будет в методе ниже
                PlayerScoreManager.Instance.ChangePlayerScore(WinScoreReward);
            }
            else
            {
                PlayerScoreManager.Instance.ChangePlayerScore((int)(WinScoreReward * 0.7f));
            }
        }
        else
        {
            if (YandexGame.savesData.roadMapStep == RoadMapStep)
            {
                PlayerScoreManager.Instance.ChangePlayerScore(DefeatScoreReward);
            }
            else
            {
                PlayerScoreManager.Instance.ChangePlayerScore((int)(DefeatScoreReward * 0.7f));
            }
        }
    }
}
