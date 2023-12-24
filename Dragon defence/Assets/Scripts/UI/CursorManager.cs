using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    public CursorType currentCursorType { get; private set; }

    private Dictionary<CursorType, Texture2D> cursorByType;

    private int fireballTargetAreaMask;
    [SerializeField] private bool isFight = false;
    [SerializeField] private CursorData[] cursorDatas;

    void Start()
    {
        Instance = this;

        fireballTargetAreaMask = LayerMask.NameToLayer("fireball-target-area");
        fireballTargetAreaMask = 1 << fireballTargetAreaMask;

        cursorByType = new();
        foreach (var cursorData in cursorDatas)
        {
            cursorByType.Add(cursorData.type, cursorData.texture);
        }

        currentCursorType = CursorType.StandartClick; // чтобы курсор поменялся после следующей строчки
        ChangeCursorType(CursorType.Standart);
    }

    void Update()
    {
        if (!isFight) return;

        var targeting = TargetSelection.Instance;
        if (targeting.isOccupied && targeting.initiatorType == TotemType.Fire)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out RaycastHit hit, 50f, fireballTargetAreaMask))
            {
                ChangeCursorType(CursorType.FireClick);
                if (Input.GetMouseButtonDown(0))
                {
                    var temp = new GameObject("temp-fireball-target");
                    temp.transform.position = hit.point;
                    targeting.EndTargetSelection(temp);
                }
            }
            else
            {
                ChangeCursorType(CursorType.Fire);
            }
        }
    }

    public void ChangeCursorType(CursorType type)
    {
        if (type == currentCursorType) return;

        currentCursorType = type;
        Cursor.SetCursor(cursorByType[type], Vector2.zero, CursorMode.Auto);
    }
}
