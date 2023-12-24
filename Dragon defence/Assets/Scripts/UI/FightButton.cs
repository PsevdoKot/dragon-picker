using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FightButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;

    [SerializeField] private GameObject loadingInfoGO;
    [SerializeField] private GameObject mapMenuGO;

    [SerializeField] private int roadMapStep;
    [SerializeField] private FightParamsData fightParams;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        if (!button.IsUnityNull())
        {
            button.onClick.AddListener(LoadFightWithParams);
        }
    }

    void OnDisable()
    {
        if (!button.IsUnityNull())
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void LoadFightWithParams()
    {
        AudioManager.Instance.Play("menu-click");
        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
        mapMenuGO.SetActive(false);
        loadingInfoGO.SetActive(true);

        FightParamsManager.Instance.SetFightParams(roadMapStep, fightParams);
        SceneManager.LoadSceneAsync("DragonFight");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.enabled)
        {
            CursorManager.Instance.ChangeCursorType(CursorType.StandartClick);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
    }
}
