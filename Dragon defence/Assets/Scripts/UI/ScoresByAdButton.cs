using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoresByAdButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject scoresByAdPanelGO;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        button.onClick.AddListener(HandleClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    void Update()
    {
        if (scoresByAdPanelGO.IsUnityNull()) // после одного просмотра рекламы убираем кнопку
        {
            Destroy(gameObject);
            return;
        }
    }

    private void HandleClick()
    {
        scoresByAdPanelGO.SetActive(!scoresByAdPanelGO.activeInHierarchy);
    }
}
