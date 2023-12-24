using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        if (!button.IsUnityNull())
        {
            button.onClick.AddListener(() => AudioManager.Instance.Play("menu-click"));
        }
    }

    void OnDisable()
    {
        if (!button.IsUnityNull())
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.enabled) return;

        CursorManager.Instance.ChangeCursorType(CursorType.StandartClick);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.enabled) return;

        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
    }
}
