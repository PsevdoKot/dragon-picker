using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private bool isButton = true;

    void Awake()
    {
        if (isButton)
        {
            button = GetComponent<Button>();
        }
    }

    void OnEnable()
    {
        if (button != null)
        {
            button.onClick.AddListener(() => AudioManager.Instance.Play("menu-click"));
        }
    }

    void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isButton && !button.enabled) return;

        CursorManager.Instance.ChangeCursorType(CursorType.StandartClick);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isButton && !button.enabled) return;

        CursorManager.Instance.ChangeCursorType(CursorType.Standart);
    }
}
